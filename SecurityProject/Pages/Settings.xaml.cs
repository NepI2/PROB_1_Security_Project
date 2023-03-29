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
            StaticData.DefaultAESKeys = result;          

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
        }

        private void ChoosingFilesFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(StaticData.DefaultFileToOpen, "Folders|*.none");
            if (result != null)
            {
                MessageBox.Show("Folder picked");
            }
            StaticData.DefaultFileToOpen = result;
        }
    }
}
