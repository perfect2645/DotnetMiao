using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Decode
{
    internal class JmbDecode
    {

        private const string StrToDecode = "35408a1b492a05d1d25441bbf75cdc819c518b1241c0693911d662e31bd886c51b71d93eac44654463733d575bbbcf619e5adc50f7ba40b27270b6ee515b7ed6";

        public JmbDecode() 
        {
            Decode();
        }

        private void Decode()
        {
            var deResult = DecryptAes(StrToDecode);
        }


        private string DecryptAes(string _str)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("shen=new#su@app-");

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

        private string EncryptAes(string source)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("shen=new#su@app-");
            byte[] sourceArray = UTF8Encoding.UTF8.GetBytes(source);

            Aes rm = Aes.Create();
            rm.Key = keyArray;
            rm.Mode = CipherMode.ECB;
            rm.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rm.CreateEncryptor();
            byte[] encryptArray = cTransform.TransformFinalBlock(sourceArray, 0, sourceArray.Length);
            var enSource = Convert.ToBase64String(encryptArray, 0, encryptArray.Length);
            return enSource;
        }
    }
}
