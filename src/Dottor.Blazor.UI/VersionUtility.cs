namespace Dottor.Blazor.UI;

using System.Reflection;

public static class VersionUtility
{
    private static string? _version;

#if DEBUG
    public static string GetVersion()
    {
        if (string.IsNullOrWhiteSpace(_version))
        {
            _version = Guid.NewGuid().ToString();
        }
        return _version;
    }
#else
    public static string GetVersion()
    {
        if (string.IsNullOrWhiteSpace(_version))
        {
            _version = Assembly.GetExecutingAssembly()
                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                        .InformationalVersion;
        }
        return _version;
    }
#endif

}
