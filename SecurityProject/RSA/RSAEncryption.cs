using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurityProject.RSA
{
    public class RSAEncryption
    {
        private RSACryptoServiceProvider rsa;
        public RSAEncryption()
        {
            rsa = new RSACryptoServiceProvider();
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
