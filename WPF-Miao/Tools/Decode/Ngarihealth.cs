using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Decode
{
    internal class Ngarihealth
    {
        public Ngarihealth()
        {
            UserInfo();
        }

        private void UserInfo()
        {
            var userRequestEn = "nsHEeqPBp9wC0B6fHgDJl+ZzFuY/sZyJp6RgwbLdDvSGeXozmTBZx2KRSXN9+oQT5zqCmQMLh+5VybIBzzXYnA==";

            var userRequestDe = Decrypt22(userRequestEn);

            var userResponse = "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";

            var userDe = Decrypt22(userResponse);
        }

        public string Decrypt22(string _str)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("vss7db748e839799");

            Aes rm = Aes.Create();
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
