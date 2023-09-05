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

        public static string ToBase64String(string source, int length = 32)
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

        public static string Base64Decode(string code)
        {
            return (Encoding.Default.GetString(Convert.FromBase64String(code)));
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

        public static string ToUnicode(this string normalStr, bool isIgnoreSpace = true, bool isUpperCaseU = false)
        {
            if (string.IsNullOrEmpty(normalStr))
            {
                return string.Empty;
            }

            StringBuilder strResult = new StringBuilder();

            void func(int index)
            {
                if (isUpperCaseU)
                {
                    strResult.Append("\\U");
                }
                else
                {
                    strResult.Append("\\u");
                }
                strResult.Append(((int)normalStr[index]).ToString("x").PadLeft(4, '0'));
            }

            for (int i = 0; i < normalStr.Length; i++)
            {
                if (isIgnoreSpace)
                {
                    if (normalStr[i] == ' ')
                    {
                        strResult.Append(" ");
                    }
                    else
                    {
                        func(i);
                    }
                }
                else
                {
                    func(i);
                }
            }
            return strResult.ToString();
        }

        public static string DeCodePkcs7(string data)
        {
            try
            {
                if (data.Contains("u003d"))
                {
                    data = data.Replace("\"", "");
                    data = data.Replace("\\u003d\\u003d", "\u003d\u003d");
                }

                byte[] keyArray = Encoding.UTF8.GetBytes("W3Xzhty3Jvlpl!yl");
                byte[] toEncryptArray = Convert.FromBase64String(data);
                var rDel = new RijndaelManaged
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return null;
            }
        }


        public static String GetMD5_10(string str)
        {
            // 创建MD5对象
            MD5 md5 = MD5.Create();
            // 需要将字符串转换成字节数组
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            // 返回一个加密好的字节数组
            byte[] MD5Buffer = md5.ComputeHash(buffer);

            // 将字节数组每个元素ToString()  10进制
            // 3244185981728979115075721453575112
            string s = "";
            foreach (var item in MD5Buffer)
            {
                s += item.ToString();
            }
            return s;
        }

        public static string GetMD5_16(string str)
        {
            // 创建MD5对象
            MD5 md5 = MD5.Create();
            // 需要将字符串转换成字节数组
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            // 返回一个加密好的字节数组
            byte[] MD5Buffer = md5.ComputeHash(buffer);

            // 将字节数组每个元素ToString(x) 16进制
            //202cb962ac5975b964b7152d234b70
            string s2 = "";
            foreach (var item in MD5Buffer)
            {
                s2 += item.ToString("x");
            }
            return s2;
        }
        public static string GetMD5_32(string str)
        {
            // 创建MD5对象
            MD5 md5 = MD5.Create();
            // 需要将字符串转换成字节数组
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            // 返回一个加密好的字节数组
            byte[] MD5Buffer = md5.ComputeHash(buffer);

            // 将字节数组每个元素ToString(x) 16进制
            //202cb962ac5975b964b7152d234b70
            string s2 = "";
            foreach (var item in MD5Buffer)
            {
                s2 += item.ToString("x2");
            }
            return s2;
        }
    }
}
