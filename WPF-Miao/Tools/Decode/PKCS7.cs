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
            var data = "eb9be4151e173ef8032d2a785dd2d462767818f345eceff4121e8330811601dfb8ea7afe3767be284ed113f54e80cafb249e5f043f018d8f1ffeaa705050a3000a758854358ea414efb9bf9e4b389d3ca58bedbf5e564bf59e0dc2862d02eaf51c11edd7ccbe46e0b715533281c8e39e908af0fbddfe1cba61bc19b37871c09eabe0f5e1620a3132a2209f86dd70f46f8655e652fa778d4097c824dee2ec8c8ab707ab041712f8ab9f448b57c20965a335969e687d875a2968e6b6c4af5428df318da8708840c762c7b3f0ad14477755557eab678648524f714f9f40770ddf9fb897b87e1aa7e0d38c3a72640c7853b58673039aaf669295111e5e3d48c8f19659cd2782a9360d50bdceb785dbaed9cc55c0c5577eae53141289987d5ceb601f08adbc29031e676bba801115abe446c9e65474edccaf5224f6014d0a02730d6e35e9330ca670dc9a4f8e7b504ee8869996917660879d2963f8e410016d94e40aa00c4545cbb6e1b5fcfcd68a3c108ee85c74757e6343ee803b7ddc6188caf33a87101ce3c20e205f8603ad9536532632e8dd32a75e62f801b6200860c8d92d287c2bff44b8526067a6bc4fd3f281ec886c50c6e8a57108f9c7e90e29278c39ba82f9a915982cca011cfdf723be6d16a272b0e941e323f4f8ebba81221b6d333d722280df9b7d20e4eb9ce402d9579160ed5c9fbfce2959d532972c8a5f6abd5d1758304fee71aef81b27404661344a797cf80e313c52f7ad46d444c02cf29fbda7261e09fd86f0d710486564cba86f625d08ab6971af73e2643e79efe3f03d226657d1b1ccb2ba725131c70583246bf9";
            DeCodeDes(data);
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
