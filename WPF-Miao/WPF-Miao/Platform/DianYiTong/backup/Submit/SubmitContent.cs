using HttpProcessor.Client;
using HttpProcessor.JsonFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WPF_Miao.Platform.DianYiTong.Submit
{
    internal class SubmitContent : HttpMessageContentBase
    {
        public SubmitContent(string requestUrl) : base(requestUrl)
        {
        }
    }
}
