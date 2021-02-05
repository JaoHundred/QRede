using QRede.Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace QRede.Services
{
    public static class EncryptionService
    {
        public static string EncryptPassword(string wifiString, out string key)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    string base64Guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    key = base64Guid;

                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(base64Guid));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(wifiString);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);

                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public static string DecryptPassword(string encryptedWifiString, string key)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(encryptedWifiString);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
    }
}
