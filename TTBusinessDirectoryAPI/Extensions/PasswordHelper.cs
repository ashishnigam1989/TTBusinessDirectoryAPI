using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using System.Text;
using System;

namespace TTBusinessDirectoryAPI.Extensions
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (var sha512 = SHA512.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                byte[] hashBytes = sha512.ComputeHash(passwordBytes);

                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("X2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
