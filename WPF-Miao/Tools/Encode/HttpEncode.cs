using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tools.Encode
{
    internal class HttpEncode
    {
        public HttpEncode() 
        {
            //geztsnbq
            var str = "13940897525";
            var en = HttpUtility.JavaScriptStringEncode(str);


            var byt = Encoding.UTF8.GetBytes(str);

            var base64 = Convert.ToBase64String(byt);
        }

    }
}
