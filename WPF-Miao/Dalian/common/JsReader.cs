

using JsInteract.ClearJs;
using System.Collections.Generic;

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

        public static object GetERequestData(string path, Dictionary<string, object> parameters)
        {
            var result = JsEngine.Engine.Script.GetERequestData(path, parameters);
            return result;
        }

    }
}
