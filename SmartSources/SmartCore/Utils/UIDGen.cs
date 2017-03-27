using System.Security.Cryptography;
using System.Text;

namespace Smart.Utils
{
    public static class UIDGen
    {
        private static readonly RNGCryptoServiceProvider _cryptoService = new RNGCryptoServiceProvider();
        private static readonly char[] _alphabet = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public static string Get(int size = 8)
        {                           
            var data = new byte[size];
            _cryptoService.GetNonZeroBytes(data);
                
            var result = new StringBuilder(size);
            foreach (var b in data)
                result.Append(_alphabet[b % (_alphabet.Length - 1)]);

            return result.ToString();
        }
    }
}