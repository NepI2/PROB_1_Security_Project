using AES;
using Layout;
using Layout.HelpersClasses;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
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
                System.Windows.Forms.MessageBox.Show("Folder picked");
            }
            StaticData.DefaultAESKeys = result;
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
            }
            StaticData.SelectedAESFile = openPNG.FileName;
        }

        AESEncryption aesEncrypt = new AESEncryption();
        AESDecryption aesDecrypt = new AESDecryption();

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            aesEncrypt.EncryptStringToBytes_Aes();
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
