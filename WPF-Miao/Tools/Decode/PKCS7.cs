using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.Encode;

namespace Tools.Decode
{
    internal class PKCS7
    {
        public PKCS7()
        {
            var data = "pZXJhJQOFGGrbY0XMmafPr64MlFlEHuJfSj56TGeQqEA19D2IV2b0peYbO9RbFLnCu559HmBsbrso8BTIMUyFCXr+gd/J4enHC8V30krCmaZXsqVpxNaOgtUzZwSz5FBctPYGlTiZrDKp0aApbgm7JxG3SerugPcoNFecpaAIDZM/Rt1EJaCt4+8ygynopToB3/g5IzVQ6oF0L3NHn9wWgqv9wSZPdy7Bdu5VER3F1ijg/COp8KNeHe9ees3AeX8JPw2Hm6sOLYp0WHGE3+46PKVHL7h2y6sY6uKdPxXdsnHhoqR7/IB5EJsoTvcKGlUJZWgBuTrMxBHWEySqzxZEZ4ok6xODYdC5mxXb+a8B8dRb7zsLnpyxZ12TVanq7VB6VOPzCwUuWx6JVuooQbSTcoPPAaofSmEY2PxvcrvEhPfYgYu1HIdmRKGFBTLz1Dh9wi787SiBYkPTVLbkHw7NE8tP0FuU34KU38FYnh6AxKsrWIbPblT8DCb5WCner9k2fEf4tC/3RWyU7t7I2HJzc4ydNURSe+OhMZ9nC4IT0QtDaal23gVrakeTl4H5X8mMJDy6f7ZNnDMxKGsgirEVxhbUBi7lXVHiNQ+OwZqlcu0vtcqTBkVJ9H0+kIuYPa7zKJ885f9GpuL3LyHppvyBV8c2IFn5jlUKKW6BTYDNEvB4VueZHS6Gg9gVt0sfMsppDy52kX8gTO3fqRIxmKo2SiwhLsvKilSQtrXK0mYXvl1QeRr2Y2xs+CvgQPSqpulTlkg45JlXCY9V8mFSQYSDJHA7oHxaODpw7f1BBDCb3fsD5IJcGGcDJOuKWzEzekJEIIW20lF/h5haovMU+JcdqwfRq6FT5SAf+8/I0CSOdRyJSHz3/3pCj13/Qh2qoJmfWPDXEIj9CPIe6kDZNCT4JEIw/cRKjrW4SLNZ3K4KIBWwPoU5rT3vxwi1jr9Hg93JX3h6ix5IfGfGMB8Jbfa3AGCPWUvPxCUyOcsy+X5VUroqjrOmkb3xJOWDUSsmgING+NkGL2Die3o057xo9ts0g5lxj4X/OJyOpCnttGbpniXyWGVbMDisEeQBDvXWBBfajPB8Ut3l7O8tZ9shpOVTeFR9kapiFg+QhLL9e7S25Eh/1ZtTYbZQgT0CtTzbeCURw3/yHM2C4P+ZtK3tKqyFxruO6ClsYj7PYG0RhUbENPoGFclve6ty730L8qJXNvIh/cAV9q5PDjDQh7jqyc8AjnbgrlJbqd0qL7ThVqeQ7meEq/4R6imqju444PIpSzd4VtsBCCPFH/vswT6hRpiCGoubh4Nap3ZBTqLR7uXfyujGkxpJ3C9OuZ0ySs2j52i6U/lNKEaNC7U7j6TkXv3iFl53Wn9PHfr+j43mTm6DxX7sKYmgG0JalGCQUOFK9Kqz+kerTuFdArfoiI2yE3zwMPbHcFqK4f1bXv43Vb4jpf1zAHa7JKmAWjpQpWImWUcCuhvSpjKry037vlTsJ0wCQ==";
            DeCodeCBC(data);
        }
        public string DeCodePkcs7(string data)
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

        public string DeCodeDes(string data)
        {
            try
            {
                var aes = Aes.Create();
                aes.Mode = CipherMode.CBC;
                ICryptoTransform ct;
                MemoryStream ms;
                CryptoStream cs;
                byte[] byt;

                var key = "3132333435363738393041424344454631323334353637383930414243444566";
                var iv = "30313233343536373839414243444546";

                var aesDe = AesEncode.AESDecrypt(data, key, iv);

                var keyHash = Encryptor.ToMD5Hash(key);
                var ivHash = Encryptor.ToMD5Hash(iv);
                byte[] keyArray = Encoding.UTF8.GetBytes(key);
                byte[] IVArray = Encoding.UTF8.GetBytes(iv);

                ct = aes.CreateDecryptor(keyHash, ivHash);
                byt = Convert.FromBase64String(data);
                
                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                
                cs.Close();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string DeCodeCBC(string data)
        {
            try
            {
                var aes = Aes.Create();
                aes.Mode = CipherMode.CBC;
                ICryptoTransform ct;
                MemoryStream ms;
                CryptoStream cs;
                byte[] byt;

                var key = "sxlkkjoefncxmggg";
                var iv = "0682036802830600";

                var aesDe = AesEncode.AESDecrypt(data, key, iv);

                return aesDe.text;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string ConvertStringToHex(string strASCII, string separator = null)
        {
            StringBuilder sbHex = new StringBuilder();
            foreach (char chr in strASCII)
            {
                sbHex.Append(String.Format("{0:X2}", Convert.ToInt32(chr)));
                sbHex.Append(separator ?? string.Empty);
            }
            return sbHex.ToString();
        }

        public string ConvertHexToString(string HexValue, string separator = null)
        {
            HexValue = string.IsNullOrEmpty(separator) ? HexValue : HexValue.Replace(string.Empty, separator);
            StringBuilder sbStrValue = new StringBuilder();
            while (HexValue.Length > 0)
            {
                sbStrValue.Append(Convert.ToChar(Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString());
                HexValue = HexValue.Substring(2);
            }
            return sbStrValue.ToString();
        }
    }
}
