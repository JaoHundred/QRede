using QRede.Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace QRede.Services
{
    public static class EncryptionService
    {
        public static void EncryptPassword(this WifiSummary wifiSummary, string password)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    string base64Guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    wifiSummary.Key = base64Guid;

                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(base64Guid));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(password);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        wifiSummary.EncryptedPassword = Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public static string DecryptPassword(this WifiSummary wifiSummary)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(wifiSummary.Key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(wifiSummary.EncryptedPassword);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
    }
}
