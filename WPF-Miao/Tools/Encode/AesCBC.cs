using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Encode;

namespace Tools.Encode
{
    internal class AesCBC
    {
        public AesCBC()
        {
            TestAesCBC();
        }

        private void TestAesCBC()
        {
            var content = "{\"birthday\":\"2009-06-10\",\"tel\":\"13909856656\",\"sex\":1,\"cname\":\"包睿\",\"doctype\":1,\"idcard\":\"210213200906102029\",\"mxid\":\"D64fu8_eV-AqiJEkt1wdafUXCGaO-rJ2dZgOGEuJbfk\",\"date\":\"2023-12-07\",\"pid\":\"XBsAAA\",\"Ftime\":1,\"guid\":\"\"}";

            var key = "33653032303164653037343434663938";
            var iv = "31323334353637383930303030303030";
            var encodeObj = AesEncode.AESEncrypt(content, key, iv);
            var encodeContent = encodeObj.text;
        }
    }
}
