using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Xml.Linq;
using AES;
using Layout;
using Microsoft.Win32;

namespace SecurityProject.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void generateAES_Click(object sender, RoutedEventArgs e)
        {
            AESEncryption aeskey = new AESEncryption();
            if (txtGenKey.Text == "")
            {
                MessageBox.Show("Gelieve een naam mee te geven voor de key");
            }
            else
            {
                aeskey.GenerateKeys(txtGenKey.Text);
            }
            MessageBox.Show($"{txtGenKey.Text} generated");
            txtGenKey.Text = string.Empty;
        }

        private void generateRSA_Click(object sender, RoutedEventArgs e)
        {

            //using (var rsa = new RSACryptoServiceProvider())
            //{
            //    rsa.KeySize = 2048;
            //    string publicKey = rsa.ToXmlString(true);
            //    string privetKey = rsa.ToXmlString(false);
            //    rsa.PersistKeyInCsp = false;
            //    File.WriteAllText(Path.Combine(StaticData.DefaultRSAKeys, $"{name}_public.xml"), publicKey);
            //    File.WriteAllText(Path.Combine(StaticData.DefaultRSAKeys, $"{name}_private.xml"), privetKey);
            //    return true;
            //}
        }
    }
}
