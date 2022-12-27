using MSScriptControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.stringBuilder;

namespace Redhouse.Encrypt
{
    internal static class HfzEncrypt
    {

        private static object RunScript(string method, string[] pars)
        {
            ScriptControl sc = new ScriptControl();
            sc.Language = "javascript";
            string javascript1 = File.ReadAllText(@"Encrypt/hfzEncrypt.js");
            sc.AddCode(javascript1);

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
                object obj = sc.Eval(mainCons);
                return obj;

            }
            else
            {
                string mainCons = method + "()";
                object obj = sc.Eval(mainCons);
                return obj;
            }

        }


        public static string Encrypt(string key, string content)
        {
            var encodeObj = RunScript("DncryptData", new string[] { key, content });
            var encode = encodeObj.NotNullString();

            return encode;
        }
    }
}
