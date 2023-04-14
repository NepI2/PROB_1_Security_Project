using Layout.HelpersClasses;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SecurityProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            navbar.SelectedIndex = 0;
            navframe.Source = new Uri("Pages/Home.xaml", UriKind.Relative);
            LoadConfig();
        }

        private void LoadConfig()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var folderSet = false;

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["DefaultFileEncrypted"]) ||
                string.IsNullOrEmpty(ConfigurationManager.AppSettings["DefaultFileDecrypted"]) ||
                string.IsNullOrEmpty(ConfigurationManager.AppSettings["DefaultFileToOpen"]) ||
                string.IsNullOrEmpty(ConfigurationManager.AppSettings["DefaultAESKeys"]) ||
                string.IsNullOrEmpty(ConfigurationManager.AppSettings["DefaultRSAKeys"]))
            {
                StaticData.DefaultFileEncrypted = Helpers.FolderManager.SetFolderPathOrCreate("DefaultFileEncrypted");
                config.AppSettings.Settings["DefaultFileEncrypted"].Value = StaticData.DefaultFileEncrypted;

                StaticData.DefaultFileDecrypted = Helpers.FolderManager.SetFolderPathOrCreate("DefaultFileDecrypted");
                config.AppSettings.Settings["DefaultFileDecrypted"].Value = StaticData.DefaultFileDecrypted;

                StaticData.DefaultFileToOpen = Helpers.FolderManager.SetFolderPathOrCreate("DefaultFileToOpen");
                config.AppSettings.Settings["DefaultFileToOpen"].Value = StaticData.DefaultFileToOpen;

                StaticData.DefaultAESKeys = Helpers.FolderManager.SetFolderPathOrCreate("DefaultAESKeys");
                config.AppSettings.Settings["DefaultAESKeys"].Value = StaticData.DefaultAESKeys;

                StaticData.DefaultRSAKeys = Helpers.FolderManager.SetFolderPathOrCreate("DefaultRSAKeys");
                config.AppSettings.Settings["DefaultRSAKeys"].Value = StaticData.DefaultRSAKeys;
                config.Save();
            }
            else
            {
                string defaultFileAESEncrypted = ConfigurationManager.AppSettings["DefaultFileEncrypted"];
                StaticData.DefaultFileEncrypted = defaultFileAESEncrypted;

                string defaultFileAESDecrypted = ConfigurationManager.AppSettings["DefaultFileDecrypted"];
                StaticData.DefaultFileDecrypted = defaultFileAESDecrypted;

                string defaultFileToOpen = ConfigurationManager.AppSettings["DefaultFileToOpen"];
                StaticData.DefaultFileToOpen = defaultFileToOpen;

                string defaultAESKeys = ConfigurationManager.AppSettings["DefaultAESKeys"];
                StaticData.DefaultAESKeys = defaultAESKeys;

                string defaultRSAKeys = ConfigurationManager.AppSettings["DefaultRSAKeys"];
                StaticData.DefaultRSAKeys = defaultRSAKeys;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void navbar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = navbar.SelectedItem as NavButton;
            navframe.Navigate(selected.Navlink);
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}