using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SecurityProject.RSA
{
    public class RSA
    {
        private RSACryptoServiceProvider rsa;
        public RSA() 
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
            byte[] encryptedBytes = rsa.Encrypt(plainBytes, RSAEncryptionPadding.OaepSHA256);
            return encryptedBytes;
        }

        public string Decrypt(byte[] encryptedBytes, string privateKey)
        {
            rsa.FromXmlString(privateKey);
            byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA256);
            string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedText;
        }
    }
}
