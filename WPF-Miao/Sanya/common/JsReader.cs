

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
            JsEngine.InitEngine("common/crypto-js.min.js");
            JsEngine.InitEngine("common/encode.js");
        }

        public static Dictionary<string, object> DecodeAesCbc(string input)
        {
            var result = JsEngine.Engine.Script.decryptByAes(input);

            var requestDataDic = new Dictionary<string, object>();

            return requestDataDic;
        }

    }
}
