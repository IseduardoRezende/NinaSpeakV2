using System.Security.Cryptography;
using System.Text;

namespace NinaSpeakV2.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string ConvertToSHA512(this string value, string salt)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || !Guid.TryParse(salt, out _))
            {
                throw new ArgumentException("Invalid Value(s).");
            }

            var data = Encoding.UTF8.GetBytes(string.Concat(value, salt));
            return Convert.ToBase64String(SHA512.HashData(data));
        }        
    }
}
