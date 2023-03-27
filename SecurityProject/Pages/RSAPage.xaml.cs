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
using SecurityProject.RSA;
using System.IO;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;

namespace SecurityProject.Pages
{
    /// <summary>
    /// Interaction logic for RSAPage.xaml
    /// </summary>
    public partial class RSAPage : Page, INotifyPropertyChanged
    {
        public string EncryptedText { get; set; }
        public string DecryptedText { get; set; }
        private RSAEncryption _rsa = new RSAEncryption();
        private string _plainText = string.Empty;
        public RSAPage()
        {
            InitializeComponent();
            EncryptedTextBox.DataContext = this;
        }
        private void ChooseFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Select a folder";
            dialog.Filter = "Folders|*.none";
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            dialog.FileName = "RSA keys";
            if (dialog.ShowDialog() == true)
            {
                string selectedPath = System.IO.Path.GetDirectoryName(dialog.FileName);
            }
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Text Files|*.txt";
            if (dialog.ShowDialog() == true)
            {
                string filePath = dialog.FileName;
                _plainText = File.ReadAllText(filePath);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string publicKey = _rsa.GetPublicKey();
            byte[] encryptedData = _rsa.Encrypt(_plainText, publicKey);
            EncryptedText = Convert.ToBase64String(encryptedData);
            OnPropertyChanged(nameof(EncryptedText));
        }

        
        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            string privatekey = _rsa.GetPrivateKey();
            byte[] encryptedBytes = _rsa.Encrypt(_plainText, privatekey);
            EncryptedText = Convert.ToBase64String(encryptedBytes);
            OnPropertyChanged(nameof(DecryptedText));
        }
    }
}
