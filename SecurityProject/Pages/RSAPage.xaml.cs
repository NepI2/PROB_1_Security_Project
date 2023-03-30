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
    public partial class RSAPage : Page
    {
        public string EncryptedText { get; set; }
        public string DecryptedText { get; set; }
        private RSAEncryption _rsa = new RSAEncryption();
        private string _plainText = string.Empty;
        public RSAPage()
        {
            InitializeComponent();
            LoadRSAKeys();
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

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string publicKey = _rsa.GetPublicKey();
            byte[] encryptedData = _rsa.Encrypt(_plainText, publicKey);
            EncryptedText = Convert.ToBase64String(encryptedData);
            txtEncrypted.Text = EncryptedText;
        }

        
        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            string privatekey = _rsa.GetPrivateKey();
            byte[] encryptedBytes = _rsa.Encrypt(_plainText, privatekey);
            DecryptedText = Convert.ToBase64String(encryptedBytes);
            txtDecrypted.Text = DecryptedText;
        }

        private void LoadRSAKeys()
        {
            string[] keyFiles = Directory.GetFiles(StaticData.DefaultRSAKeys, "*_public.xml");
            foreach (string keyFile in keyFiles)
            {
                string keyFileName = Path.GetFileNameWithoutExtension(keyFile);
                KeyComboBox.Items.Add(keyFileName);
            }
        }

        private void KeyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KeyComboBox.SelectedItem != null)
            {
                string selectedKeyFileName = (string)KeyComboBox.SelectedItem;
                string selectedKeyFilePath = System.IO.Path.Combine(StaticData.DefaultRSAKeys, selectedKeyFileName);
                StaticData.SelectedAESKey = selectedKeyFilePath;
            }
        }
    }
}
