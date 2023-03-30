using SecurityProject.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            string value = config.AppSettings.Settings["DefaultFoldersSet"].Value;
            if (!string.IsNullOrEmpty(value))           
                StaticData.DefaultFoldersSet = bool.Parse(value);            
            // Load the saved value from the App.config file and set the DefaultAESKeys property
            string defaultAESKeys = ConfigurationManager.AppSettings["DefaultAESKeys"];
            if (!string.IsNullOrEmpty(defaultAESKeys))           
                StaticData.DefaultAESKeys = defaultAESKeys;
            

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
    }
}
