using System;
using System.Security.Cryptography;
using System.Text;

namespace WPF_Miao.Platform.yunnan.encrypt
{
    internal static class Encrypt
    {
        public static string Decrypt22(string _str)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("vss7db748e839799");
            RijndaelManaged rm = new RijndaelManaged();
            rm.Key = keyArray;
            rm.Mode = CipherMode.ECB;
            rm.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rm.CreateDecryptor();
            byte[] bytes = Convert.FromBase64String(_str);
            byte[] encryptArray = cTransform.TransformFinalBlock(bytes, 0, bytes.Length);
            _str = UTF8Encoding.UTF8.GetString(encryptArray, 0, encryptArray.Length);
            return _str;
        }
    }
}
