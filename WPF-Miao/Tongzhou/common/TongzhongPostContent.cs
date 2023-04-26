using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tongzhou.login;
using Utils;
using Utils.json;
using Tongzhou.session;
using System.Text.Json;

namespace Tongzhou.common
{
    internal class TongzhongPostContent : TongzhouContent
    {
        public string XCaMethod { get; set; }
        public string XCaMethodId { get; set; }
        public Dictionary<string, string> SecurityHeaderDic = new Dictionary<string, string>();

        public TongzhongPostContent(string method, string methodId, TongzhouLogin user) : base(user)
        {
            XCaMethod = method;
            XCaMethodId= methodId;
        }

        protected void BuildSignHeaders()
        {
            var jsonContents = GetJsonContent(Content);

            AddHeader("X-Ca-Signature", "");
            AddHeader("X-Ca-Nonce", User.XCaNonce);
            AddHeader("X-Ca-Timestamp", User.Timestamp);
            AddHeader("X-Service-Encrypt", "1");
            AddHeader("X-Service-Method", XCaMethod);
            AddHeader("X-Ca-Key", "ngari-wx");
            AddHeader("X-Content-MD5", "");
            AddHeader("encoding", "utf-8");
            AddHeader("X-Service-Id", XCaMethodId);
        }

        protected void SetXContentMD5(string contentJson)
        {
            var base64Md5 = Encryptor.ToBase64Md5(contentJson);
            SecurityHeaderDic[Constants.XContentMD5] = base64Md5;
        }

        protected void BuildXCaSignature()
        {
            var sb = new StringBuilder();
            foreach (var header in SecurityHeaderDic)
            {
                sb.Append(header.Key.ToLower());
                sb.Append(":");
                sb.Append(header.Value);
                sb.Append("&");
            }
            var textToSign = sb.ToString().TrimEnd('&');
            var sha256 = Encryptor.HmacSHA256(textToSign, Constants.CASECRET);
            var signature = Encryptor.EncryptBase64(sha256);

            SecurityHeaderDic.Add(Constants.XCaSignature, signature);
        }

        public string GetJsonContent(Dictionary<string, object> contents, bool isArr = false)
        {
            var jsonContent = JsonSerializer.Serialize(contents, JsonEncoder.JsonOption);
            if (isArr)
            {
                jsonContent = $"[{jsonContent}]";
            }

            return jsonContent;
        }
    }
}
