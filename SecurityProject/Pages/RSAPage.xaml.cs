using SecurityProject.RSA;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

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
            LoadEncryptedFilesComboBox.Items.Clear();
            string[] keyFiles = Directory.GetFiles(StaticData.DefaultFileEncrypted, "encrypted_*.xml");
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
            KeyComboBoxRSA.Items.Clear();
            string[] keyFiles = Directory.GetFiles(StaticData.DefaultRSAKeys, "*_public.xml");
            foreach (string keyFile in keyFiles)
            {
                string keyFileName = Path.GetFileNameWithoutExtension(keyFile);
                KeyComboBoxRSA.Items.Add(keyFileName);
            }
        }

        private void LoadAESKeys()
        {
            KeyComboBox.Items.Clear();
            string[] keyFiles = Directory.GetFiles(StaticData.DefaultAESKeys, "*_aes.xml");
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
                txtEncrypted.Text = _plainText;
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
                saveFileDialog1.InitialDirectory = Path.GetFullPath(StaticData.DefaultFileEncrypted);
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
                        LoadAESKeys();
                        LoadEncryptedKeys();
                    }
                }
            }
        }

        private void LoadEncryptedFilesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedKeyFileName = (string)LoadEncryptedFilesComboBox.SelectedItem;
            string selectedKeyFilePath = Path.Combine(StaticData.DefaultFileEncrypted, $"{selectedKeyFileName}.xml");
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

        private void SaveDecryptedAES_Click(object sender, RoutedEventArgs e)
        {
            if (txtEncrypted.Text != "")
            {
                System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog1.InitialDirectory = Path.GetFullPath(StaticData.DefaultAESKeys);
                saveFileDialog1.Filter = "XML|*_aes.xml";
                saveFileDialog1.Title = "Save your encrypted AES key";
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var name = Path.GetFileName(saveFileDialog1.FileName);
                    var splitName = name.Split('.');
                    var encrypterName = $"decrypted_{splitName[0]}_aes.{splitName[1]}";
                    if (saveFileDialog1.FileName != "")
                    {
                        string directoryPath = Path.GetDirectoryName(saveFileDialog1.FileName);
                        File.WriteAllText(Path.Combine(directoryPath, encrypterName), DecryptedText);
                        LoadAESKeys();
                    }
                }
            }
        }
    }
}