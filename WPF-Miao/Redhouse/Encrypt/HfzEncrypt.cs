using MSScriptControl;
using System.IO;
using System.Linq;
using Utils.stringBuilder;

namespace Redhouse.Encrypt
{
    internal static class HfzEncrypt
    {
        private static readonly ScriptControl ScriptControl;

        static HfzEncrypt()
        {
            ScriptControl = new ScriptControl();
            ScriptControl.Language = "javascript";
            string javascript1 = File.ReadAllText(@"Encrypt/hfzEncrypt.js");
            ScriptControl.AddCode(javascript1);
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
