using Microsoft.Extensions.Configuration;

namespace OPENSCSMAccessM.Config
{
    public static class WorkerSettings
    {
        private static readonly IConfiguration _config;

        static WorkerSettings()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string ScsmServer
            => _config["SCSM:Server"] ?? "WIN-6IC5HULM18O";
        public static string Enviroment
            => _config["SCSM:Enviroment"] ?? "WIN-6IC5HULM18O";
        public static string ControlerDomain
            => _config["SCSM:ControlerDomain"] ?? "Domain";
        public static string DataBase
            => _config["SCSM:DataBase"] ?? "ServiceManager";
        public static string ScsmSdkPath
            => _config["SCSM:SdkPath"]
               ?? @"C:\Program Files\Microsoft System Center\Service Manager";

        public static string Database
            => _config["Database:Name"] ?? "ServiceManager";

        public static string DbServer
            => _config["Database:Server"] ?? string.Empty;

        public static string SCSMConnection
            => _config["Database:ConnectionString"] ?? string.Empty;

        public static string ScriptsPath
            => _config["Paths:Scripts"] ?? AppDomain.CurrentDomain.BaseDirectory;

        public static string LogPath
            => _config["Paths:Logs"] ?? AppDomain.CurrentDomain.BaseDirectory;

        public static string WorkerUrl
            => _config["Worker:Url"] ?? "http://localhost:5001";
    }
}

