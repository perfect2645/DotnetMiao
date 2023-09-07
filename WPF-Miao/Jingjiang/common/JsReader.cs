using MSScriptControl;
using System.IO;
using System.Linq;
using Utils.stringBuilder;

namespace Jingjiang.Encrypt
{
    internal static class JsReader
    {
        private static readonly ScriptControl ScriptControl;

        static JsReader()
        {
            ScriptControl = new ScriptControl();
            ScriptControl.Language = "javascript";
            string encodeJs = File.ReadAllText(@"common/encode.js");
            ScriptControl.AddCode(encodeJs);

            string encodeSource = File.ReadAllText(@"common/encSource.js");
            ScriptControl.AddCode(encodeSource);
        }

        public static string GetEncodeString(string str)
        {
            var result = RunScript("Encode", new string[] { str });

            return result.NotNullString();
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
