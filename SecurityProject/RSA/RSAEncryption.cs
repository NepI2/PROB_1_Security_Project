using Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Xml;
using System.Xml.Serialization;

namespace SecurityProject.RSA
{
    public class RSAEncryption
    {
        private RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        private RSAParameters _privKey;
        private RSAParameters _pubKey;
        public RSAEncryption()
        {
            _privKey = rsa.ExportParameters(true);
            _pubKey = rsa.ExportParameters(false);
        }

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
        public string GetPublicKey()
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _pubKey);
            return sw.ToString();
            //return rsa.ToXmlString(false);
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
            //rsa.ImportParameters(_pubKey);
            //var data = Encoding.UTF8.GetBytes(plainText);
            //var cypher = rsa.Encrypt(data, false);
            //return Convert.ToBase64String(cypher);
            try
            {
                rsa.ImportParameters(_pubKey);
                var data = Encoding.Default.GetBytes(plainText);
                var cypher = rsa.Encrypt(data, false);
                return Convert.ToBase64String(cypher);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Encryption failed: " + ex.Message);
                return null; // or throw a custom exception
            }           
        }

        public string Decrypt(string cypherText, string privateKey)
        {
            try
            {
                var dataBytes = Convert.FromBase64String(cypherText);
                rsa.ImportParameters(_privKey);
                var plainText = rsa.Decrypt(dataBytes, false);
                return Encoding.Default.GetString(plainText);
            }
            catch (CryptographicException ex)
            {
                MessageBox.Show("Decryption failed: " + ex.Message);
                return null; // or throw a custom exception
            }

            //rsa.FromXmlString(privateKey);
            //byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, true);
            //string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
            //return decryptedText;
        }
    }
}
