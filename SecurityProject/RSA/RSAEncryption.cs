using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace SecurityProject.RSA
{
    public class RSAEncryption
    {
        private RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

        public void GenerateKeys(string name)
        {
            string publicKeyPath = Path.Combine(StaticData.DefaultRSAKeys, $"{name}_public.xml");
            string privateKeyPath = Path.Combine(StaticData.DefaultRSAKeys, $"{name}_private.xml");
            if (File.Exists(publicKeyPath) && File.Exists(privateKeyPath))
            {
                return;
            }
            rsa.KeySize = 2048;
            string publicKey = rsa.ToXmlString(false);
            string privateKey = rsa.ToXmlString(true);
            rsa.PersistKeyInCsp = false;
            File.WriteAllText(publicKeyPath, publicKey);
            File.WriteAllText(privateKeyPath, privateKey);
        }

        public string GetPublicKey(string name)
        {
            string publicKeyPath = Path.Combine(StaticData.DefaultRSAKeys, $"{name}.xml");
            if (File.Exists(publicKeyPath))
            {
                return File.ReadAllText(publicKeyPath);
            }
            else
            {
                throw new FileNotFoundException("Private key file not found.", publicKeyPath);
            }
        }

        public string GetPrivateKey(string name)
        {
            string privateKeyName = name.Replace("_public", "_private");
            string privateKeyPath = Path.Combine(StaticData.DefaultRSAKeys, $"{privateKeyName}.xml");
            if (File.Exists(privateKeyPath))
            {
                return File.ReadAllText(privateKeyPath);
            }
            else
            {
                throw new FileNotFoundException("Private key file not found.", privateKeyPath);
            }
        }

        public string Encrypt(string plainText, string publicKey)
        {
            try
            {
                rsa.FromXmlString(publicKey);
                var data = Encoding.Default.GetBytes(plainText);
                var cypher = rsa.Encrypt(data, false);
                return Convert.ToBase64String(cypher);
            }
            catch (CryptographicException ex)
            {
                MessageBox.Show("Encryption failed: " + ex.Message);
                return null;
            }
        }

        public string Decrypt(string cypherText, string privateKey)
        {
            try
            {
                var dataBytes = Convert.FromBase64String(cypherText);
                rsa.FromXmlString(privateKey);
                var plainText = rsa.Decrypt(dataBytes, false);
                return Encoding.Default.GetString(plainText);
            }
            catch (CryptographicException ex)
            {
                MessageBox.Show("Decryption failed: " + ex.Message);
                return null;
            }
        }
    }
}