using System.Configuration;

namespace SecurityProject
{
    public static class StaticData
    {
        public static string? DefaultFileAESEncrypted { get; set; } 
        public static string? DefaultFileAESDecrypted { get; set; }
        public static string? DefaultFileRSAEncrypted { get; set; } 
        public static string? DefaultFileRSADecrypted { get; set; }
        public static string? DefaultFileToOpen { get; set; }
        public static string? DefaultAESKeys { get; set; }
        public static string? DefaultRSAKeys { get; set; }
        public static string? SelectedAESKey { get; set; }
        public static string? SelectedAESFile { get; set; }

        public static bool DefaultFoldersSet { get; set; } = false;


        public static void UpdateDefaultFoldersSet()
        {
            if (DefaultFileAESEncrypted != null &&
                DefaultFileAESDecrypted != null &&
                DefaultFileRSAEncrypted != null &&
                DefaultFileRSADecrypted != null &&
                DefaultFileToOpen != null &&
                DefaultAESKeys != null &&
                DefaultRSAKeys != null)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["DefaultFoldersSet"].Value = "true";
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                DefaultFoldersSet = true;
            }
            else
            {
                DefaultFoldersSet = false;
            }
        }
    }
}
