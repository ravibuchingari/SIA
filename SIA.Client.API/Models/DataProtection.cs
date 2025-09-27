using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SIA.Client.API.Models
{
    public class DataProtection
    {
        internal static string EncryptWithIV(string plainText, string[] keys)
        {
            if (plainText == null || plainText.Length <= 0)
                return string.Empty;
            byte[] bytesPlainText = Encoding.UTF8.GetBytes(plainText);
            using Aes cipher = Aes.Create();
            cipher.Key = Encoding.UTF8.GetBytes(keys[0]);
            cipher.IV = Encoding.UTF8.GetBytes(keys[1]);
            ICryptoTransform cryptoTransform = cipher.CreateEncryptor();
            byte[] bytesCipherText = cryptoTransform.TransformFinalBlock(bytesPlainText, 0, bytesPlainText.Length);
            return Convert.ToBase64String(bytesCipherText);
        }

        internal static string DecryptWithIV(string cipherText, string[] keys)
        {
            if (cipherText == null || cipherText.Length <= 0)
                return string.Empty;

            byte[] bytesCipherText = Convert.FromBase64String(cipherText);
            using Aes cipher = Aes.Create();
            cipher.Key = Encoding.UTF8.GetBytes(keys[0]);
            cipher.IV = Encoding.UTF8.GetBytes(keys[1]);
            ICryptoTransform cryptoTransform = cipher.CreateDecryptor();
            byte[] bytesPlainText = cryptoTransform.TransformFinalBlock(bytesCipherText, 0, bytesCipherText.Length);
            return Encoding.UTF8.GetString(bytesPlainText);
        }

        internal static string UrlEncode(string plainText, string[] keys) => HttpUtility.UrlEncode(Convert.ToBase64String(Encoding.ASCII.GetBytes(EncryptWithIV(plainText, keys))), Encoding.UTF8);

        internal static string UrlDecode(string encryptedText, string[] keys) => DecryptWithIV(Encoding.ASCII.GetString(Convert.FromBase64String(HttpUtility.UrlDecode(encryptedText, Encoding.UTF8))), keys);

        internal static byte[] GetSaltHasPassword(byte[] password, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            int passwordLength = password.Length;
            int saltLength = salt.Length;
            byte[] saltPasswordBytes = new byte[passwordLength + saltLength];

            for (int i = 0; i < passwordLength; i++)
                saltPasswordBytes[i] = password[i];

            for (int i = 0; i < saltLength; i++)
                saltPasswordBytes[passwordLength + i] = salt[i];

            return algorithm.ComputeHash(saltPasswordBytes);
        }

        internal static byte[] GenerateRandomNumber(int length)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] randomNumber = new byte[length];
            rng.GetBytes(randomNumber);
            return randomNumber;
        }

        internal static Guid StringToGuid(string input)
        {
            byte[] hash = MD5.HashData(Encoding.UTF8.GetBytes(input));
            return new Guid(hash);
        }

    }
}
