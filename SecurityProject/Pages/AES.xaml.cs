using AES;
using Layout;
using Layout.HelpersClasses;
using System;
using System.IO;
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
        }
        private void ChooseFolder_Click(object sender, RoutedEventArgs e)
        {
            //var dialog = new OpenFileDialog();
            //dialog.Title = "Select a folder";
            //dialog.Filter = "Folders|*.none";
            //dialog.CheckFileExists = false;
            //dialog.CheckPathExists = true;
            //dialog.FileName = "AES keys";
            //if (dialog.ShowDialog() == true)
            //{
            //    // User selected a folder
            //    string selectedPath = System.IO.Path.GetDirectoryName(dialog.FileName);
            //    // Save or load keys from the selected path
            //}

            var result = Helpers.SelectingFolder(StaticData.DefaultAESKeys, "Folders|*.none");
            if (result != null)
            {
                StaticData.DefaultAESKeys = result;
                System.Windows.Forms.MessageBox.Show("Folder picked");
            }
            
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openPNG = new OpenFileDialog();

            //openPNG.InitialDirectory = StaticData.DefaultFileToOpen;

            openPNG.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
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

        AESEncryption aesEncrypt = new AESEncryption();
        AESDecryption aesDecrypt = new AESDecryption();

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            if (StaticData.SelectedAESFile != null && StaticData.SelectedAESKey != null)
            {
                // Fade out the image
                DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
                imgResult.BeginAnimation(Image.OpacityProperty, animation);

                aesEncrypt.EncryptStringToBytes_Aes();
                // Fade in the image
                animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1));
                imgResult.BeginAnimation(Image.OpacityProperty, animation);
            }
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {

            if (StaticData.SelectedAESFile != null && StaticData.SelectedAESKey != null)
            {
                // Fade out the image
                DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
                imgResult.BeginAnimation(Image.OpacityProperty, animation);

                byte[] decrypted = aesDecrypt.DecryptBytesFromBytes_Aes();
                using (MemoryStream ms = new MemoryStream(decrypted))
                {
                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(ms))
                    {
                        string temp = Microsoft.VisualBasic.Interaction.InputBox("Enter file name", "Decryption ready!", "");
                        image.Save($"{StaticData.DefaultFileAESDecrypted}/{Path.GetFileNameWithoutExtension(temp)}.png", System.Drawing.Imaging.ImageFormat.Png);
                        imgResult.Source = new BitmapImage(new Uri($"{StaticData.DefaultFileAESDecrypted}/{Path.GetFileNameWithoutExtension(temp)}.png"));
                    }
                }
                // Fade in the image
                animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1));
                imgResult.BeginAnimation(Image.OpacityProperty, animation);
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
            string[] keyFiles = Directory.GetFiles(StaticData.DefaultAESKeys, "*.xml");

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
            }
        }
    }
}
