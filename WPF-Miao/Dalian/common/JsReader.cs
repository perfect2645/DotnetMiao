

using JsInteract.ClearJs;
using System.Collections.Generic;
using Utils;

namespace Dalian.common
{
    internal static class JsReader
    {
        private static readonly JsEngine JsEngine;

        static JsReader()
        {
            JsEngine = new JsEngine();
            JsEngine.InitEngine("common/encode.js",
                "common/sm4_index.js",
                "common/sm3.js",
                "common/sm3_index.js",
               "common/common.js");
        }

        public static Dictionary<string, object> GetRequestData(string path, string userId, Dictionary<string, object> parameters)
        {
            var jsonParameters = parameters.ToJson();
            var result = JsEngine.Engine.Script.GetRequestData(path, userId, jsonParameters);

            var requestDataDic = new Dictionary<string, object>();
            var requestData = (string)result.request.requestData;
            var nonce = (string)result.nonce;
            var timestamp = (long)result.timestamp;
            var signature = (string)result.signature;
            requestDataDic.AddOrUpdate("requestData", requestData);
            requestDataDic.AddOrUpdate("nonce", nonce);
            requestDataDic.AddOrUpdate("timestamp", timestamp);
            requestDataDic.AddOrUpdate("signature", signature);

            return requestDataDic;
        }

    }
}
