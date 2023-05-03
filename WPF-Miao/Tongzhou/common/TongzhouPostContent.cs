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
using Utils.datetime;

namespace Tongzhou.common
{
    internal class TongzhouPostContent : TongzhouContent
    {
        public Dictionary<string, string> SecurityHeaderDic = new Dictionary<string, string>();

        public TongzhouPostContent(string method, string methodId, TongzhouLogin user) : base(user)
        {
            var timestamp = User.Timestamp;
            SecurityHeaderDic.Add(Constants.XCaKey, Constants.CAKEY);
            SecurityHeaderDic.Add(Constants.XCaNonce, BuildXCaNonce(timestamp));
            SecurityHeaderDic.Add(Constants.XCaTimestamp, timestamp);
            SecurityHeaderDic.Add(Constants.XContentMD5, string.Empty);
            SecurityHeaderDic.Add(Constants.XServiceId, methodId);
            SecurityHeaderDic.Add(Constants.XServiceMethod, method);
        }

        protected void BuildSignHeaders()
        {
            var jsonContents = GetJsonContent(Content);
            SetXContentMD5(jsonContents);
            BuildXCaSignature();

            AddHeader("X-Ca-Signature", SecurityHeaderDic[Constants.XCaSignature]);
            AddHeader("X-Ca-Nonce", SecurityHeaderDic[Constants.XCaNonce]);
            AddHeader("X-Ca-Timestamp", SecurityHeaderDic[Constants.XCaTimestamp]);
            AddHeader("X-Service-Encrypt", "1");
            AddHeader("X-Service-Method", SecurityHeaderDic[Constants.XServiceMethod]);
            AddHeader("X-Ca-Key", "ngari-wx");
            AddHeader("X-Content-MD5", SecurityHeaderDic[Constants.XContentMD5]);
            AddHeader("encoding", "utf-8");
            AddHeader("X-Service-Id", SecurityHeaderDic[Constants.XServiceId]);
        }

        protected string BuildXCaNonce(string timestamp)
        {
            var xCaNonce = $"{User.Uuid}{timestamp}{MainSession.LocalTimeOffset}";
            return xCaNonce;
        }

        protected void SetXContentMD5(string contentJson)
        {
            if (contentJson.Equals("{}"))
            {
                contentJson = "[]";
            }
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

        public string GetJsonContent(Dictionary<string, object> contents)
        {
            var jsonContent = JsonSerializer.Serialize(contents, JsonEncoder.JsonOption);
            jsonContent = $"{ContentPrefix}{jsonContent}{ContentSuffix}";
            return jsonContent;
        }
    }
}
