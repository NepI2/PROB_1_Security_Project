using Layout.HelpersClasses;
using System.Windows;
using System.Windows.Controls;

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

        private void SettingPahtLables()
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
            Helpers.UpdateFolderPathSettings("DefaultRSAKeys", result);
            StaticData.DefaultRSAKeys = result;
            RSAKeyPlaceholder.Text = result;
        }

        private void AESKeysFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(StaticData.DefaultAESKeys, "Folders|*.none");
            Helpers.UpdateFolderPathSettings("DefaultAESKeys", result);
            StaticData.DefaultAESKeys = result;
            AESKeyPlaceholder.Text = result;
        }

        private void EncriptedFilesFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(StaticData.DefaultFileEncrypted, "Folders|*.none");
            Helpers.UpdateFolderPathSettings("DefaultFileEncrypted", result);
            StaticData.DefaultFileEncrypted = result;
            EncryptedFilesPlaceholder.Text = result;
        }

        private void DecriptedFilesFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(StaticData.DefaultFileDecrypted, "Folders|*.none");
            Helpers.UpdateFolderPathSettings("DefaultFileDecrypted", result);
            StaticData.DefaultFileDecrypted = result;
            DecryptedFilesPlaceholder.Text = result;
        }

        private void ChoosingFilesFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(StaticData.DefaultFileToOpen, "Folders|*.none");
            Helpers.UpdateFolderPathSettings("DefaultFileToOpen", result);
            StaticData.DefaultFileToOpen = result;
            ChoosingFilesPlaceholder.Text = result;
        }
    }
}