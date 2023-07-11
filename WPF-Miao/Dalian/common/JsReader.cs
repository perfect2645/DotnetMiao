using MSScriptControl;
using System.IO;
using System.Linq;
using Utils.stringBuilder;

namespace Dalian.common
{
    internal static class JsReader
    {
        private static readonly ScriptControl ScriptControl;

        static JsReader()
        {
            ScriptControl = new ScriptControl();
            ScriptControl.Language = "javascript";

            string json2 = File.ReadAllText(@"common/json2.js");
            ScriptControl.AddCode(json2);

            string encodeJs = File.ReadAllText(@"common/encode.js");
            ScriptControl.AddCode(encodeJs);

            string sm4_index = File.ReadAllText(@"common/sm4_index.js");
            ScriptControl.AddCode(sm4_index);


            string sm3 = File.ReadAllText(@"common/sm3.js");
            ScriptControl.AddCode(sm3);

            string sm3_index = File.ReadAllText(@"common/sm3_index.js");
            ScriptControl.AddCode(sm3_index);

            string commonJs = File.ReadAllText(@"common/common.js");
            ScriptControl.AddCode(commonJs);

        }

        public static object GetEncodeString(string path, string parameters )
        {
            var result = RunScript("Encode", new string[] { path, parameters });

            return result;
        }

        private static object RunScript(string method, string[] pars)
        {
            if (pars.Any())
            {
                string temppars = "";
                foreach (string s in pars)
                {
                    temppars += "'" + s + "',";
                }

                temppars = temppars.Remove(temppars.LastIndexOf(","));
                temppars += ")";
                string mainCons = method + "(" + temppars;
                object obj = ScriptControl.Eval(mainCons);
                return obj;
            }
            else
            {
                string mainCons = method + "()";
                object obj = ScriptControl.Eval(mainCons);
                return obj;
            }
        }


        public static string Decrypt(string content)
        {
            var step1 = RunScript("Decode", new string[] { content });
            var step1Str = step1.NotNullString();

            var step2 = RunScript("decrypt2test", new string[] { step1Str });

            return step1Str;
        }
    }
}
