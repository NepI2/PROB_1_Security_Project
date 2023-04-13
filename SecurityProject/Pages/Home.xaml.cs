using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;
using AES;
using Layout;
using Microsoft.Win32;
using SecurityProject.RSA;

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
            if (txtGenKey.Text != "")
            {
                aeskey.GenerateKeys(txtGenKey.Text);
                MessageBox.Show($"Key {txtGenKey.Text} is generated");
            }
            else
            {
                MessageBox.Show("Please enter a name for the key");
            }         
            txtGenKey.Text = string.Empty;
        }

        private void generateRSA_Click(object sender, RoutedEventArgs e)
        {
            RSAEncryption rsaKey = new RSAEncryption();
            if (txtGenKey.Text != "")
            {
                MessageBox.Show("Please enter a name for the key");
                rsaKey.GenerateKeys(txtGenKey.Text);
                MessageBox.Show($"Key {txtGenKey.Text} is generated");
            }
            else
            {
                MessageBox.Show("Please enter a name for the key");
            }           
            txtGenKey.Text = string.Empty;
        }
    }
}
