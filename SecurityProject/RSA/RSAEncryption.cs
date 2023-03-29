using Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SecurityProject.RSA
{
    public class RSAEncryption
    {
        private RSACryptoServiceProvider rsa;
        public RSAEncryption()
        {
            rsa = new RSACryptoServiceProvider();
        }

        public void GenerateKeys(string name)
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
        public string GetPublicKey()
        {
            return rsa.ToXmlString(false);
        }
        public string GetPrivateKey()
        {
            return rsa.ToXmlString(true);
        }

        public byte[] Encrypt(string plainText, string publicKey)
        {
            rsa.FromXmlString(publicKey);
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = rsa.Encrypt(plainBytes, true);
            return encryptedBytes;
        }

        public string Decrypt(byte[] encryptedBytes, string privateKey)
        {
            rsa.FromXmlString(privateKey);
            byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, true);
            string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedText;
        }
    }
}
