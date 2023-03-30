using Layout.HelpersClasses;
using Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace SecurityProject.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void RSAKeysFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(StaticData.DefaultRSAKeys, "Folders|*.none");
            if (result != null)
            {
                MessageBox.Show("Folder picked");
            }
            // Set the DefaultAESKeys property to the selected folder path
            StaticData.DefaultRSAKeys = result;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["DefaultRSAKeys"].Value = StaticData.DefaultRSAKeys;
            config.Save(ConfigurationSaveMode.Modified);
            StaticData.UpdateDefaultFoldersSet();

        }

        private void AESKeysFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(StaticData.DefaultAESKeys, "Folders|*.none");
            if (result != null)
            {
                MessageBox.Show("Folder picked");
            }
            StaticData.DefaultAESKeys = result;
            // Save the value to the App.config file
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["DefaultAESKeys"].Value = StaticData.DefaultAESKeys;
            config.Save(ConfigurationSaveMode.Modified);
            StaticData.UpdateDefaultFoldersSet();
        }

        private void EncriptedFilesFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(StaticData.DefaultFileRSADecrypted, "Folders|*.none");
            if (result != null)
            {
                MessageBox.Show("Folder picked");
            }
            StaticData.DefaultFileRSAEncrypted = result;            
            StaticData.DefaultFileAESEncrypted = result;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["DefaultFileRSAEncrypted"].Value = StaticData.DefaultFileRSAEncrypted;
            config.AppSettings.Settings["DefaultFileAESEncrypted"].Value = StaticData.DefaultFileAESEncrypted;           
            config.Save(ConfigurationSaveMode.Modified);
            StaticData.UpdateDefaultFoldersSet();
        }

        private void DecriptedFilesFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(StaticData.DefaultFileRSAEncrypted, "Folders|*.none");
            if (result != null)
            {
                MessageBox.Show("Folder picked");
            }
            StaticData.DefaultFileRSADecrypted = result;
            StaticData.DefaultFileAESDecrypted = result;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["DefaultFileRSADecrypted"].Value = StaticData.DefaultFileRSADecrypted;
            config.AppSettings.Settings["DefaultFileAESDecrypted"].Value = StaticData.DefaultFileAESDecrypted;
            config.Save(ConfigurationSaveMode.Modified);
            StaticData.UpdateDefaultFoldersSet();
        }

        private void ChoosingFilesFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(StaticData.DefaultFileToOpen, "Folders|*.none");
            if (result != null)
            {
                MessageBox.Show("Folder picked");
            }
            StaticData.DefaultFileToOpen = result;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["DefaultFileToOpen"].Value = StaticData.DefaultFileToOpen;
            config.Save(ConfigurationSaveMode.Modified);
            StaticData.UpdateDefaultFoldersSet();
        }
    }
}
