using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Utility
{
    public class HashGenerator
    {
        public static string Hash(string password, string key)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(key),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));            
        }

        public static string RandomHash()
        {
            byte[] salt = Guid.NewGuid().ToByteArray();
            return Convert.ToBase64String(salt);
        }
    }
}
