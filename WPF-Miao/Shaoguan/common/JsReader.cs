using MSScriptControl;
using System.IO;
using System.Linq;
using Utils.stringBuilder;

namespace Huangshi.Encrypt
{
    internal static class JsReader
    {
        private static readonly ScriptControl ScriptControl;

        static JsReader()
        {
            ScriptControl = new ScriptControl();
            ScriptControl.Language = "javascript";
            string javascript1 = File.ReadAllText(@"common/encode.js");
            ScriptControl.AddCode(javascript1);
        }

        public static string GetEncodeString(string str)
        {
            var result = RunScript("_0x558503", new string[] { str });

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


        public static string Decrypt(string key, string content)
        {
            var decodeObj = RunScript("DncryptData", new string[] { key, content });
            var decode = decodeObj.NotNullString();

            return decode;
        }
    }
}
