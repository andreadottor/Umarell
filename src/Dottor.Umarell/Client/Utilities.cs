namespace Dottor.Umarell.Client
{
    using System.Reflection;

    public static class Utilities
    {
        private static string _version;

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
    }
}
