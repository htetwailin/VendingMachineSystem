using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace VendingMachineSystem.Common
{
    public class EncryptionHelper
    {
        private static readonly byte[] Salt = new byte[] { 0x26, 0x19, 0x82, 0x5E, 0x5F, 0x5A, 0x0A, 0x3A, 0x54, 0x4D, 0x2A, 0x36, 0x72 };
        public static string Encrypt(string plainText, string password)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            using (var passwordDeriveBytes = new Rfc2898DeriveBytes(password, Salt))
            {
                byte[] keyBytes = passwordDeriveBytes.GetBytes(32);
                byte[] ivBytes = passwordDeriveBytes.GetBytes(16);

                using (var aes = Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.IV = ivBytes;

                    using (var memoryStream = new MemoryStream())
                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();

                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        public static string Decrypt(string encryptedText, string password)
        {
            byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);

            using (var passwordDeriveBytes = new Rfc2898DeriveBytes(password, Salt))
            {
                byte[] keyBytes = passwordDeriveBytes.GetBytes(32);
                byte[] ivBytes = passwordDeriveBytes.GetBytes(16);

                using (var aes = Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.IV = ivBytes;

                    using (var memoryStream = new MemoryStream())
                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(encryptedTextBytes, 0, encryptedTextBytes.Length);
                        cryptoStream.FlushFinalBlock();

                        byte[] decryptedTextBytes = memoryStream.ToArray();
                        return Encoding.UTF8.GetString(decryptedTextBytes, 0, decryptedTextBytes.Length);
                    }
                }
            }
        }
    }
}
