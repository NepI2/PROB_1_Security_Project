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
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (DefaultFileAESEncrypted != null)
            {
                config.AppSettings.Settings["DefaultFileAESEncrypted"].Value = DefaultFileAESEncrypted;
            }

            if (DefaultFileAESDecrypted != null)
            {
                config.AppSettings.Settings["DefaultFileAESDecrypted"].Value = DefaultFileAESDecrypted;
            }

            if (DefaultFileRSAEncrypted != null)
            {
                config.AppSettings.Settings["DefaultFileRSAEncrypted"].Value = DefaultFileRSAEncrypted;
            }

            if (DefaultFileRSADecrypted != null)
            {
                config.AppSettings.Settings["DefaultFileRSADecrypted"].Value = DefaultFileRSADecrypted;
            }

            if (DefaultFileToOpen != null)
            {
                config.AppSettings.Settings["DefaultFileToOpen"].Value = DefaultFileToOpen;
            }

            if (DefaultAESKeys != null)
            {
                config.AppSettings.Settings["DefaultAESKeys"].Value = DefaultAESKeys;
            }

            if (DefaultRSAKeys != null)
            {
                config.AppSettings.Settings["DefaultRSAKeys"].Value = DefaultRSAKeys;
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            UpdateDefaultFoldersSetStatus();
        }

        private static void UpdateDefaultFoldersSetStatus()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (DefaultFileAESEncrypted != null &&
                DefaultFileAESDecrypted != null &&
                DefaultFileRSAEncrypted != null &&
                DefaultFileRSADecrypted != null &&
                DefaultFileToOpen != null &&
                DefaultAESKeys != null &&
                DefaultRSAKeys != null)
            {
                config.AppSettings.Settings["DefaultFoldersSet"].Value = "true";
                DefaultFoldersSet = true;
            }
            else
            {
                config.AppSettings.Settings["DefaultFoldersSet"].Value = "false";
                DefaultFoldersSet = false;
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }


    }
}

