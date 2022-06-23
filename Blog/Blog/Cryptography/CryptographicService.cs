using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Blog.Cryptography
{
    public class CryptographicService : ICryptographicService
    {

        private const string Key = "KIhVazAM9PcciXgyN2an/Vkp4bytF+B/INSbBrQSTmY=";
        private const int Iterations = 50000;

        /// <summary>
        /// A2 - 
        /// </summary>
        /// <param name="password">The password as a string, no special encoding needed.</param>
        /// <param name="salt">The salt value as any arbitrary string. No special encoding needed.</param>
        /// <returns></returns>
        public string HashPassword(string password, string salt)
        {

            return Storage_A2(password, salt);

        }

        /// <summary>
        /// A2 - 
        private static string Storage_A2(string password, string salt)
        {
            // turn our salt into a base64 encoded string
            // create crypto.
            using (var aes = Aes.Create())
            {
                aes.IV = System.Text.Encoding.UTF8.GetBytes(salt).Take(16).ToArray();
                aes.KeySize = 256;
                aes.Key = Convert.FromBase64String(Key);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(password);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }



    }
}