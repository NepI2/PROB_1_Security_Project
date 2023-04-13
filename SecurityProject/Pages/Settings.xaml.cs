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
using MS.WindowsAPICodePack.Internal;

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
            SettingPahtLables();
        }

        void SettingPahtLables()
        {
            RSAKeyPlaceholder.Text = StaticData.DefaultRSAKeys;
            AESKeyPlaceholder.Text = StaticData.DefaultAESKeys;
            EncryptedFilesPlaceholder.Text = StaticData.DefaultFileEncrypted;
            DecryptedFilesPlaceholder.Text = StaticData.DefaultFileDecrypted;
            ChoosingFilesPlaceholder.Text = StaticData.DefaultFileToOpen;
        }
        private void RSAKeysFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(StaticData.DefaultRSAKeys, "Folders|*.none");
            StaticData.UpdateFolderPathSettings("DefaultRSAKeys");
            StaticData.DefaultRSAKeys = result;
            RSAKeyPlaceholder.Text = result;

        }

        private void AESKeysFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(StaticData.DefaultAESKeys, "Folders|*.none");
            StaticData.UpdateFolderPathSettings("DefaultAESKeys");
            StaticData.DefaultAESKeys = result;
            AESKeyPlaceholder.Text = result;
        }

        private void EncriptedFilesFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(StaticData.DefaultFileDecrypted, "Folders|*.none");
            StaticData.UpdateFolderPathSettings("DefaultFileDecrypted");
            StaticData.DefaultFileEncrypted = result;            
            EncryptedFilesPlaceholder.Text = result;
        }

        private void DecriptedFilesFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(StaticData.DefaultFileDecrypted, "Folders|*.none");
            StaticData.UpdateFolderPathSettings("DefaultFileDecrypted");
            StaticData.DefaultFileDecrypted = result;
            DecryptedFilesPlaceholder.Text = result;
        }

        private void ChoosingFilesFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(StaticData.DefaultFileToOpen, "Folders|*.none");
            StaticData.UpdateFolderPathSettings("DefaultFileToOpen");
            StaticData.DefaultFileToOpen = result;
            ChoosingFilesPlaceholder.Text = result;
        }
    }
}
