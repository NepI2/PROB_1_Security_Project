using SecurityProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;

namespace AES
{
    public class AESEncryption
    {
        public void GenerateKeys(string name)
        {
            Aes myAes = Aes.Create();
            string base64Key = System.Convert.ToBase64String(myAes.Key);
            string base64IV = System.Convert.ToBase64String(myAes.IV);

            //write keys as xml
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("AesData");
            root.SetAttribute("Key", base64Key);
            root.SetAttribute("IV", base64IV);
            doc.AppendChild(root);

            // Save XML document to file
            doc.Save($"{StaticData.DefaultAESKeys}/{name}.xml");
        }
        public byte[] EncryptStringToBytes_Aes()
        {
            byte[] Key;
            byte[] IV;

            FileStream inputStream = new FileStream(StaticData.SelectedAESFile, FileMode.Open);

            // load the XML file
            XDocument doc = XDocument.Load(StaticData.SelectedAESKey);

            // query the XML using LINQ to XML
            IEnumerable<XElement> elements = doc.Root.Elements("AesData.xml");

            // loop through the elements
            foreach (XElement element in elements)
            {
                Key = null;
                IV = null;
            }

            // get key and iv from xml file
            Key = Convert.FromBase64String(doc.Root.Attribute("Key").Value);
            IV = Convert.FromBase64String(doc.Root.Attribute("IV").Value);

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                string base64Key;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (BinaryReader reader = new BinaryReader(inputStream))
                        {
                            byte[] buffer = new byte[1024];
                            int bytesRead;
                            while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                csEncrypt.Write(buffer, 0, bytesRead);
                            }
                            csEncrypt.FlushFinalBlock();
                            base64Key = System.Convert.ToBase64String(msEncrypt.ToArray());
                            //open XMLFile and add the encrypted key to it
                            doc.Root.SetAttributeValue("CipherText", base64Key);
                            doc.Save(StaticData.SelectedAESKey);
                            return msEncrypt.ToArray();
                        }
                    }
                }
            }
        }
    }
}
