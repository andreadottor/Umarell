namespace Dottor.Umarell.MauiBlazor.Services
{
    using Microsoft.AspNetCore.Components.Authorization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class ExternalAuthStateProvider : AuthenticationStateProvider
    {
        private readonly Task<AuthenticationState> _authenticationState;

        public ExternalAuthStateProvider(AuthenticatedUser user) =>
            _authenticationState = Task.FromResult(new AuthenticationState(user.Principal));

        public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
            _authenticationState;
    }

    public class AuthenticatedUser
    {
        public ClaimsPrincipal Principal { get; set; } = new();
    }
}
