

using JsInteract.ClearJs;
using System.Collections.Generic;
using Utils;

namespace Sanya.common
{
    internal static class JsReader
    {
        private static readonly JsEngine JsEngine;

        static JsReader()
        {
            JsEngine = new JsEngine();
            JsEngine.InitEngine("common/encode.js");
        }

        public static Dictionary<string, object> GetZoeParams(string encodeZoe)
        {
            var result = JsEngine.Engine.Script.GetZoeParams(encodeZoe);

            var requestDataDic = new Dictionary<string, object>();
            //requestDataDic.AddOrUpdate("requestData", requestData);
            //requestDataDic.AddOrUpdate("nonce", nonce);
            //requestDataDic.AddOrUpdate("timestamp", timestamp);
            //requestDataDic.AddOrUpdate("signature", signature);

            return requestDataDic;
        }

    }
}
