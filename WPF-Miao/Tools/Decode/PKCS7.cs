using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Decode
{
    internal class PKCS7
    {
        public string DeCodeNormal(string data)
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
    }
}
