namespace Dottor.Umarell.Client.Services;

using Dottor.Umarell.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

public class HostAuthenticationStateProvider : AuthenticationStateProvider
{
    private static readonly TimeSpan _userCacheRefreshInterval = TimeSpan.FromSeconds(60);

    private readonly IServiceProvider                         _serviceProvider;
    private readonly NavigationManager                        _navigation;
    private readonly ILogger<HostAuthenticationStateProvider> _logger;

    private DateTimeOffset  _userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);
    private ClaimsPrincipal _cachedUser    = new(new ClaimsIdentity());

    public HostAuthenticationStateProvider(NavigationManager navigation, 
                                           ILogger<HostAuthenticationStateProvider> logger, 
                                           IServiceProvider serviceProvider)
    {
        _navigation = navigation;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync() 
        => new AuthenticationState(await GetUser(useCache: true));

    private async ValueTask<ClaimsPrincipal> GetUser(bool useCache = false)
    {
        var now = DateTimeOffset.Now;
        if (useCache && now < _userLastCheck + _userCacheRefreshInterval)
        {
            return _cachedUser;
        }

        _cachedUser = await FetchUser();
        _userLastCheck = now;

        return _cachedUser;
    }

    private async ValueTask<ClaimsPrincipal> FetchUser()
    {
        using var scope = _serviceProvider.CreateScope();
        var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();

        UserInfo? user = null;

        try
        {
            user = await httpClient.GetFromJsonAsync<UserInfo>("api/User");
        }
        catch (Exception exc)
        {
            _logger.LogWarning(exc, "Fetching user failed.");
        }

        if (user == null || !user.IsAuthenticated)
            return new ClaimsPrincipal(new ClaimsIdentity());

        var identity = new ClaimsIdentity(
            nameof(HostAuthenticationStateProvider),
            user.NameClaimType,
            user.RoleClaimType);

        if (user.Claims != null)
        {
            foreach (var claim in user.Claims)
            {
                identity.AddClaim(new Claim(claim.Type, claim.Value));
            }
        }

        return new ClaimsPrincipal(identity);
    }
}
