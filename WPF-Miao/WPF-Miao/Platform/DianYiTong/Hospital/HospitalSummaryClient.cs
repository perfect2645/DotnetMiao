using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Miao.Platform.DianYiTong.Hospital
{
    internal class HospitalSummaryClient : HttpClientBase
    {
        public HospitalSummaryClient(HttpClient client) : base(client)
        {

        }

        #region Search

        public void Search(HttpClientContentBase content)
        {
            Search(content, PostSearch);
        }

        private void PostSearch(HttpDicResponse dicResponse)
        {

        }

        #endregion Search
    }
}
