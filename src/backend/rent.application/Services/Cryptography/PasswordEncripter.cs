using System.Security.Cryptography;

namespace rent.application.Services.Cryptography
{
    public class PasswordEncripter
    {
        private readonly string _additionalKey;
        public PasswordEncripter(string additionalKey) => _additionalKey = additionalKey;
   
        public string Encrypt(string password)
        {
            var newPass = $"{password}{_additionalKey}";
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA512.HashData(bytes);

            return StringBytes(hashBytes);
        }

        private static string StringBytes(byte[] hashBytes)
        {
            var sb = new System.Text.StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
