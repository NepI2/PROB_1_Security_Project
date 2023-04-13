using Microsoft.VisualBasic.ApplicationServices;
using SecurityProject.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
            navbar.SelectedIndex= 0;
            navframe.Source = new Uri("Pages/Home.xaml", UriKind.Relative);
            LoadConfig();
        }

        private void LoadConfig()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Load the saved value from the App.config file and set the DefaultFoldersSet property
            string value = config.AppSettings.Settings["DefaultFoldersSet"].Value;
            if (!string.IsNullOrEmpty(value))
                StaticData.DefaultFoldersSet = bool.Parse(value);

            string defaultFileAESEncrypted = ConfigurationManager.AppSettings["DefaultFileAESEncrypted"];
            if (!string.IsNullOrEmpty(defaultFileAESEncrypted))
                StaticData.DefaultFileAESEncrypted = defaultFileAESEncrypted;

            string defaultFileAESDecrypted = ConfigurationManager.AppSettings["DefaultFileAESDecrypted"];
            if (!string.IsNullOrEmpty(defaultFileAESDecrypted))
                StaticData.DefaultFileAESDecrypted = defaultFileAESDecrypted;

            string defaultFileRSAEncrypted = ConfigurationManager.AppSettings["DefaultFileRSAEncrypted"];
            if (!string.IsNullOrEmpty(defaultFileRSAEncrypted))
                StaticData.DefaultFileRSAEncrypted = defaultFileRSAEncrypted;

            string defaultFileRSADecrypted = ConfigurationManager.AppSettings["DefaultFileRSADecrypted"];
            if (!string.IsNullOrEmpty(defaultFileRSADecrypted))
                StaticData.DefaultFileRSADecrypted = defaultFileRSADecrypted;

            string defaultFileToOpen = ConfigurationManager.AppSettings["DefaultFileToOpen"];
            if (!string.IsNullOrEmpty(defaultFileToOpen))
                StaticData.DefaultFileToOpen = defaultFileToOpen;

            string defaultAESKeys = ConfigurationManager.AppSettings["DefaultAESKeys"];
            if (!string.IsNullOrEmpty(defaultAESKeys))
                StaticData.DefaultAESKeys = defaultAESKeys;

            string defaultRSAKeys = ConfigurationManager.AppSettings["DefaultRSAKeys"];
            if (!string.IsNullOrEmpty(defaultRSAKeys))
                StaticData.DefaultRSAKeys = defaultRSAKeys;

            //string selectedAESKey = ConfigurationManager.AppSettings["SelectedAESKey"];
            //if (!string.IsNullOrEmpty(selectedAESKey))
            //    StaticData.SelectedAESKey = selectedAESKey;

            //string selectedRSAKey = ConfigurationManager.AppSettings["SelectedRSAKey"];
            //if (!string.IsNullOrEmpty(selectedRSAKey))
            //    StaticData.SelectedRSAKey = selectedRSAKey;

            //string selectedAESFile = ConfigurationManager.AppSettings["SelectedAESFile"];
            //if (!string.IsNullOrEmpty(selectedAESFile))
            //    StaticData.SelectedAESFile = selectedAESFile;

            else
            {
                StaticData.DefaultFileAESEncrypted = FolderManager.SetFolderPathOrCreate("DefaultFileAESEncrypted");
                config.AppSettings.Settings["DefaultFileAESEncrypted"].Value = StaticData.DefaultFileAESEncrypted;

                StaticData.DefaultFileAESDecrypted = FolderManager.SetFolderPathOrCreate("DefaultFileAESDecrypted");
                config.AppSettings.Settings["DefaultFileAESDecrypted"].Value = StaticData.DefaultFileAESDecrypted;

                StaticData.DefaultFileRSAEncrypted = FolderManager.SetFolderPathOrCreate("DefaultFileRSAEncrypted");
                config.AppSettings.Settings["DefaultFileRSAEncrypted"].Value = StaticData.DefaultFileAESEncrypted;

                StaticData.DefaultFileRSADecrypted = FolderManager.SetFolderPathOrCreate("DefaultFileRSADecrypted");
                config.AppSettings.Settings["DefaultFileRSADecrypted"].Value = StaticData.DefaultFileAESEncrypted;

                StaticData.DefaultFileToOpen = FolderManager.SetFolderPathOrCreate("DefaultFileToOpen");
                config.AppSettings.Settings["DefaultFileToOpen"].Value = StaticData.DefaultFileAESEncrypted;

                StaticData.DefaultAESKeys = FolderManager.SetFolderPathOrCreate("DefaultAESKeys");
                config.AppSettings.Settings["DefaultAESKeys"].Value = StaticData.DefaultFileAESEncrypted;

                StaticData.DefaultRSAKeys = FolderManager.SetFolderPathOrCreate("DefaultRSAKeys");
                config.AppSettings.Settings["DefaultRSAKeys"].Value = StaticData.DefaultFileAESEncrypted;

                config.AppSettings.Settings["DefaultFoldersSet"].Value = "true";
                    StaticData.DefaultFoldersSet = true;

                config.Save();
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

            if (selected.RequiresDefaultFolders && !StaticData.DefaultFoldersSet)
            {
                e.Handled = true;
                MessageBox.Show("Please set all default folders first.");
                return;
            }

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


        public static class FolderManager
        {
            public static string FolderPath { get; private set; }

            public static string SetFolderPathOrCreate(string folderName)
            {
                string fullPath = Path.Combine("Default Folders", folderName);

                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }

                FolderPath = Path.GetRelativePath(Directory.GetCurrentDirectory(), fullPath);

                return FolderPath;
            }
        }
    }
}
