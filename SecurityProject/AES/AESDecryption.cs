using SecurityProject;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace AES
{
    public class AESDecryption
    {
        public byte[] DecryptBytesFromBytes_Aes()
        {
            byte[] Key;
            byte[] IV;
            byte[] cipherText;

            // load the XML file
            XDocument doc = XDocument.Load(StaticData.SelectedAESKey);
            XDocument doc2 = XDocument.Load(StaticData.SelectedAESCipher);

            // get key, IV, and cipherText from xml file
            Key = Convert.FromBase64String(doc.Root.Attribute("Key").Value);
            IV = Convert.FromBase64String(doc.Root.Attribute("IV").Value);
            cipherText = Convert.FromBase64String(doc2.Root.Attribute("CipherText").Value);

            // Create an Aes object with the specified key and IV
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (MemoryStream msPlain = new MemoryStream())
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a memory stream.
                            csDecrypt.CopyTo(msPlain);
                            return msPlain.ToArray();
                        }
                    }
                }
            }
        }
    }
}
