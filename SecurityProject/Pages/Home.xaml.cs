using AES;
using SecurityProject.RSA;
using System.Windows;
using System.Windows.Controls;

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
            if (string.IsNullOrEmpty(txtGenKey.Text))
            {
                MessageBox.Show("Please enter a name for the key");
            }
            else
            {
                aeskey.GenerateKeys(txtGenKey.Text);
                MessageBox.Show($"Key {txtGenKey.Text} is generated");
            }
            txtGenKey.Text = string.Empty;
        }

        private void generateRSA_Click(object sender, RoutedEventArgs e)
        {
            RSAEncryption rsaKey = new RSAEncryption();
            if (string.IsNullOrEmpty(txtGenKey.Text))
            {
                MessageBox.Show("Please enter a name for the key");
            }
            else
            {
                rsaKey.GenerateKeys(txtGenKey.Text);
                MessageBox.Show($"Key {txtGenKey.Text} is generated");
            }
            txtGenKey.Text = string.Empty;
        }
    }
}