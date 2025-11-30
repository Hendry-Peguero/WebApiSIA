using System;
using System.Security.Cryptography;
using System.Text;
using WebApiSIA.Core.Application.Interfaces.Helpers;

namespace WebApiSIA.Core.Application.Helper
{
    public class Md5Helper : IMd5Helper
    {
        public string GenerateMd5(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
        public bool VerifyMd5(string input, string md5Hash)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (md5Hash == null) throw new ArgumentNullException(nameof(md5Hash));

            var computedHash = GenerateMd5(input);
            return string.Equals(computedHash, md5Hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
