using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace FNMusicUserApiLibrary.Utils
{
    public class GetHash
    {

        public static string HashCode(string value)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte hashbytes in bytes)
                {
                    stringBuilder.Append(hashbytes.ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }
        
    }
}
