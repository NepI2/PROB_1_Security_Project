using Microsoft.Win32;
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

namespace SecurityProject.Pages
{
    /// <summary>
    /// Interaction logic for AES.xaml
    /// </summary>
    public partial class AES : Page
    {
        public AES()
        {
            InitializeComponent();
        }
        private void ChooseFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Select a folder";
            dialog.Filter = "Folders|*.none";
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            dialog.FileName = "AES keys";
            if (dialog.ShowDialog() == true)
            {
                // User selected a folder
                string selectedPath = System.IO.Path.GetDirectoryName(dialog.FileName);
                // Save or load keys from the selected path
            }
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
