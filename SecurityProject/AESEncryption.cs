using System.IO;
using System.Security.Cryptography;

namespace SecurityProject
{
    public class AESEncryption
    {
        public string[] GenerateKey()
        {
            byte[] key = new byte[32];
            byte[] iv = new byte[16];

            // Generate a random key and IV
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
                rng.GetBytes(iv);
            }

            //convert to base64
            string base64Key = System.Convert.ToBase64String(key);
            string base64IV = System.Convert.ToBase64String(iv);

            //write both keys to filestream
            using (FileStream fs = new FileStream("keys.txt", FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(base64Key);
                    sw.WriteLine(base64IV);
                }
            }

            //return both keys
            return new string[] { base64Key, base64IV };
        }
    }
}
