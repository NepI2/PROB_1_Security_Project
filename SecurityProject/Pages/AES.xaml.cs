using AES;
using Layout.HelpersClasses;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

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
            LoadAESKeys();
            LoadAESCypherTexts();
        }

        private void ChooseFolder_Click(object sender, RoutedEventArgs e)
        {
            var result = Helpers.SelectingFolder(Path.GetFullPath(StaticData.DefaultAESKeys), "Folders|*.none");
            if (result != null)
            {
                StaticData.DefaultAESKeys = result;
                System.Windows.Forms.MessageBox.Show("Folder picked");
            }
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openPNG = new OpenFileDialog();
            openPNG.InitialDirectory = Path.GetFullPath(StaticData.DefaultFileToOpen);
            openPNG.Filter = "png files (*.png)|*.png";
            openPNG.FilterIndex = 2;
            openPNG.RestoreDirectory = true;

            if (openPNG.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StaticData.SelectedAESFile = openPNG.FileName;
                //show image before encryption
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(openPNG.FileName);
                bitmap.EndInit();
                imgResult.Source = bitmap;
            }
        }

        private AESEncryption aesEncrypt = new AESEncryption();
        private AESDecryption aesDecrypt = new AESDecryption();

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            if (StaticData.SelectedAESFile != null && StaticData.SelectedAESKey != null)
            {
                // Fade out the image
                DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
                imgResult.BeginAnimation(Image.OpacityProperty, animation);
                try
                {
                    string name = Microsoft.VisualBasic.Interaction.InputBox("Enter file name for cipher file", "Cipher file", "");
                    if (name != "")
                    {
                        aesEncrypt.EncryptStringToBytes_Aes(name);
                        LoadAESKeys();
                    }
                    else
                    {
                        throw new CryptographicException("Cipher file name is empty");
                    }

                }
                catch (CryptographicException ex)
                {
                    System.Windows.Forms.MessageBox.Show("Decryption failed: " + ex.Message);
                }
                // Fade in the image
                animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1));
                imgResult.BeginAnimation(Image.OpacityProperty, animation);
                LoadAESCypherTexts();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please check that you have the key and the cypher text chosen");
            }
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (StaticData.SelectedAESKey != null && StaticData.SelectedAESCipher != null)
            {
                // Fade out the image
                DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
                imgResult.BeginAnimation(Image.OpacityProperty, animation);

                try
                {
                    byte[] decrypted = aesDecrypt.DecryptBytesFromBytes_Aes();
                    using (MemoryStream ms = new MemoryStream(decrypted))
                    {
                        using (System.Drawing.Image image = System.Drawing.Image.FromStream(ms))
                        {
                            string temp = Microsoft.VisualBasic.Interaction.InputBox("Enter file name", "Decryption ready!", "");
                            image.Save($"{StaticData.DefaultFileDecrypted}/{Path.GetFileNameWithoutExtension(temp)}.png", System.Drawing.Imaging.ImageFormat.Png);
                            imgResult.Source = new BitmapImage(new Uri($"{Path.Combine(Path.GetFullPath(StaticData.DefaultFileDecrypted), $"{temp}.png")}"));
                        }
                    }
                }
                catch (CryptographicException ex)
                {
                    System.Windows.Forms.MessageBox.Show("Decryption failed: " + ex.Message);
                }

                // Fade in the image
                animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1));
                imgResult.BeginAnimation(Image.OpacityProperty, animation);
                LoadAESCypherTexts();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please check that you have the key and the cypher text chosen");
            }
        }

        private void SelectKey_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openXML = new OpenFileDialog();

            openXML.InitialDirectory = StaticData.DefaultAESKeys;
            openXML.Filter = "xml files (*.xml)|*.xml";
            openXML.FilterIndex = 2;
            openXML.RestoreDirectory = true;

            if (openXML.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
            StaticData.SelectedAESKey = openXML.FileName;
        }

        private void LoadAESKeys()
        {
            // Get the AES key files from the default folder
            string[] keyFiles = Directory.GetFiles(StaticData.DefaultAESKeys, "*_aes.xml");

            // Add the file names to the ComboBox
            foreach (string keyFile in keyFiles)
            {
                KeyComboBox.Items.Add(System.IO.Path.GetFileName(keyFile));
                CipherComboBox.Items.Add(System.IO.Path.GetFileName(keyFile));
            }
        }

        private void KeyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KeyComboBox.SelectedItem != null)
            {
                string selectedKeyFileName = (string)KeyComboBox.SelectedItem;
                string selectedKeyFilePath = Path.Combine(StaticData.DefaultAESKeys, selectedKeyFileName);
                StaticData.SelectedAESKey = selectedKeyFilePath;
            }
        }

        private void CipherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CipherComboBox.SelectedItem != null)
            {
                string selectedKeyFileName = (string)CipherComboBox.SelectedItem;
                string selectedKeyFilePath = Path.Combine(StaticData.DefaultFileEncrypted, $"{selectedKeyFileName}.xml");
                StaticData.SelectedAESCipher = selectedKeyFilePath;
            }
        }

        private void LoadAESCypherTexts()
        {
            CipherComboBox.Items.Clear();
            string[] keyFiles = Directory.GetFiles(StaticData.DefaultFileEncrypted, "*_ciphertext_aes.xml");
            foreach (string keyFile in keyFiles)
            {
                string keyFileName = Path.GetFileNameWithoutExtension(keyFile);
                CipherComboBox.Items.Add(keyFileName);
            }
        }
    }
}