﻿using Humanizer.Bytes;
using System.Security.Cryptography;
using System.Text;

namespace Server.Services
{
    public static class PasswordManager
    {
        private const int keySize = 64;
        private const int iterations = 350000;
        private static readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA512;

        public static string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, iterations, hashAlgorithmName, keySize);

            return Convert.ToHexString(hash);
        }

        public static bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithmName, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        public static string SaltToString(byte[] salt)
        {
            return Convert.ToHexString(salt);
        }
    }
}
