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
            LoadAESKeys();
            LoadEncryptedKeys();
        }

        private void LoadEncryptedKeys()
        {
            string[] keyFiles = Directory.GetFiles(StaticData.DefaultFileAESEncrypted, "encrypted_*.xml");
            foreach (string keyFile in keyFiles)
            {
                string keyFileName = Path.GetFileNameWithoutExtension(keyFile);
                LoadEncryptedFilesComboBox.Items.Add(keyFileName);
            }
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            if (KeyComboBox.SelectedItem == null || KeyComboBoxRSA.SelectedItem == null)
            {
                MessageBox.Show("Select the correct keys in the dropdowns");
            }
            else
            {
                string publicKey = _rsa.GetPublicKey(KeyComboBoxRSA.SelectedItem.ToString());
                string encryptedData = _rsa.Encrypt(_plainText, publicKey);
                EncryptedText = encryptedData;
                txtEncrypted.Text = EncryptedText;
            }
        }



        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (KeyComboBoxRSA.SelectedItem == null && EncryptedText != null)
            {
                MessageBox.Show("Select the correct keys in the dropdowns");
            }
            else if (EncryptedText != null)
            {
                string privatekey = _rsa.GetPrivateKey(KeyComboBoxRSA.SelectedItem.ToString());
                string encryptedBytes = EncryptedText;
                DecryptedText = _rsa.Decrypt(encryptedBytes, privatekey);
                txtDecrypted.Text = DecryptedText;
            }
        }



        private void LoadRSAKeys()
        {
            string[] keyFiles = Directory.GetFiles(StaticData.DefaultRSAKeys, "*_public.xml");
            foreach (string keyFile in keyFiles)
            {
                string keyFileName = Path.GetFileNameWithoutExtension(keyFile);
                KeyComboBoxRSA.Items.Add(keyFileName);
            }
        }



        private void LoadAESKeys()
        {
            // Get the RSA key files from the default folder
            string[] keyFiles = Directory.GetFiles(StaticData.DefaultAESKeys, "*_aes.xml");



            // Add the file names to the ComboBox
            foreach (string keyFile in keyFiles)
            {
                KeyComboBox.Items.Add(System.IO.Path.GetFileName(keyFile));
            }
        }



        private void KeyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KeyComboBox.SelectedItem != null)
            {
                string selectedKeyFileName = (string)KeyComboBox.SelectedItem;
                string selectedKeyFilePath = Path.Combine(StaticData.DefaultAESKeys, selectedKeyFileName);
                StaticData.SelectedAESKey = selectedKeyFilePath;
                _plainText = File.ReadAllText(selectedKeyFilePath);
            }
        }



        private void KeyComboBoxRSA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KeyComboBoxRSA.SelectedItem != null)
            {
                string selectedKeyFileName = (string)KeyComboBoxRSA.SelectedItem;
                string selectedKeyFilePath = Path.Combine(StaticData.DefaultRSAKeys, selectedKeyFileName);
                StaticData.SelectedRSAKey = selectedKeyFilePath;
            }
        }



        private void SaveAES_Click(object sender, RoutedEventArgs e)
        {
            if (txtEncrypted.Text != "")
            {
                System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog1.InitialDirectory = StaticData.DefaultFileRSAEncrypted;
                saveFileDialog1.Filter = "XML|*.xml";
                saveFileDialog1.Title = "Save your encrypted AES key";
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var name = Path.GetFileName(saveFileDialog1.FileName);
                    var encrypterName = $"encrypted_{name}";
                    if (saveFileDialog1.FileName != "")
                    {
                        string directoryPath = Path.GetDirectoryName(saveFileDialog1.FileName);
                        File.WriteAllText(Path.Combine(directoryPath, encrypterName), EncryptedText);
                    }
                }
            }
        }

        private void LoadEncryptedFilesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string selectedKeyFileName = (string)LoadEncryptedFilesComboBox.SelectedItem;
            string selectedKeyFilePath = Path.Combine(StaticData.DefaultFileAESEncrypted, $"{selectedKeyFileName}.xml");
                string encryptedBytes = EncryptedText;
            try
            {
                EncryptedText = File.ReadAllText(selectedKeyFilePath);
                txtEncrypted.Text = EncryptedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
                
        }
    }
}
