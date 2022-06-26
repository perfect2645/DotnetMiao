using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    public static class Encryptor
    {
        public static byte[] ToMD5Hash(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return new byte[] { };
            }

            HashAlgorithm? provider = CryptoConfig.CreateFromName("MD5") as HashAlgorithm;
            byte[] byteSource = Encoding.UTF8.GetBytes(source);
            byte[] hashValue = provider.ComputeHash(byteSource);
            return hashValue;
        }

        public static string ToMD5String(string source, int length = 32)
        {
            var hashValue = ToMD5Hash(source);

            var sb = new StringBuilder();
            switch (length)
            {
                case 16: // 16位密文是32位密文的9到24位字符
                    for (int i = 4; i < 12; i++)
                    {
                        sb.Append(hashValue[i].ToString("x2"));
                    }
                    break;
                case 32:
                    for (int i = 0; i < 16; i++)
                    {
                        sb.Append(hashValue[i].ToString("x2"));
                    }
                    break;
                default:
                    for (int i = 0; i < hashValue.Length; i++)
                    {
                        sb.Append(hashValue[i].ToString("x2"));
                    }
                    break;
            }

            var t = Convert.ToBase64String(hashValue);
            return sb.ToString();
        }

        public static string EncryptBase64(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }
            byte[] byteSource = Encoding.UTF8.GetBytes(source);

            var result = Convert.ToBase64String(byteSource);

            return result ?? string.Empty;
        }

        public static string EncryptBase64(byte[] hashValue)
        {
            var result = Convert.ToBase64String(hashValue);

            return result ?? string.Empty;
        }

        public static string ToBase64Md5(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            HashAlgorithm? provider = CryptoConfig.CreateFromName("MD5") as HashAlgorithm;
            byte[] byteSource = Encoding.UTF8.GetBytes(source);
            byte[] hashValue = provider.ComputeHash(byteSource);

            var base64Md5 = Convert.ToBase64String(hashValue);
            return base64Md5 ?? string.Empty;
        }

        public static byte[] HmacSHA256(string srouce, string key)
        {
            var mac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            byte[] hash = mac.ComputeHash(Encoding.UTF8.GetBytes(srouce));
            return hash;
        }
    }
}
