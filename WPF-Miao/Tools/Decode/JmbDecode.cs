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

        private const string StrToDecode = "h/Ypcxg7jOYnPEcJIRBy92do9OnCD4x0CKmBzKrSYOQRJuhF8DwsG/5CC97owGJvd4rPyCKQJEv3D8DVYHAScjNXvUALQJ6tWSnrUf3sic8RTLptUyM33gr/eU9H6gLEH/LI9ZYqsTbON8PUQnjtgmvzbY/jwcTEnPHTnC7b01Q6Q5gyqKxHxxnxlnhXPnZWKmkafpuXZBiAttBzRIxo6bId0/kMY26bgZ7uOetyoy5AabNPqIt/gBpE8gET+HYLK5v66c4kQaxZ+0r4cP0SQDBlNqYgrkKcBQjNFHtXRZNZFlZBHpTtbf3QsYzvSK2D4GXmlZKhtlqNxZW2njNmroVCDLt05DhkwDm5MCHSjChI8ZctlWefC1M/ZNBM/6ltlqde4ArJhb8YDyshh6qxXQROtLn5JhGm6z6XJPs6Q+0ufWNUIUebibkHeiQ0Wll4Z80Yvur5GODUdaP737VNktta8RAoW/ntIJD0c6lu8SClcxI+QtdLrhpFIpX8kUre73QoMI4LR3usrkhjRIoDcH6eK8uJK/1tW8lHbEe1u8vLOIHZ7cp64zb9u6KaHGSIdTMv7OJSaDTpYHyW4En9tHZLgm87uVUH3ql7RH1cx+LXp8640AHaiV6aYk59v5qCviTCmEMKrrgGlysS4NVqdUG8/S4vpodBv43wdBRK3dyAC4SHB4WMAxIcMcd0wEend7RFD2wiHNBDWjhGzwSKusQI3GOPyYmC1HnaPfyY25rXZXs7v7bZOzkUz8TQz8eZtA2/z4M+Cuk+Ilwtt2P9EcYOvOes4JnXa3u0W2Md3wvvY++gjEtVnvHEtjapdsSLsQ8I8mYREeyO4bhUCPyEVLcJyMmCNSt6Sihb9icVAMreoJchiySaZxy5QUPmFv44SpwtN7a0a975w2MEcAhQHg==";

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
