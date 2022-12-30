using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Security
{
    public class Hashing
    {
        public static string CreateSHA512(string key)
        {
            SHA512 sHA512 = SHA512.Create();
            return BitConverter.ToString
                (sHA512.ComputeHash(Encoding.UTF8.GetBytes(key))).Replace("-", "");
        }
        public static string CreateSHA256(string key)
        {
            SHA256 sHA256 = SHA256.Create();
            return BitConverter.ToString
                (sHA256.ComputeHash(Encoding.UTF8.GetBytes(key))).Replace("-", "");
        }
        public static string CreateSHA1(string key)
        {
            SHA1 sHA1 = SHA1.Create();
            return BitConverter.ToString
                (sHA1.ComputeHash(Encoding.UTF8.GetBytes(key))).Replace("-", "");
        }
        public static string CreateSHA384(string key)
        {
            SHA384 sHA384 = SHA384.Create();
            return BitConverter.ToString
                (sHA384.ComputeHash(Encoding.UTF8.GetBytes(key))).Replace("-", "");
        }
    }
}
