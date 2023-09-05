using System;
using System.Net.Http;
using System.Text;
using Tjzs.common;
using Tjzs.session;
using Utils;

namespace Tjzs.appointment
{
    internal class TokenContent : TjzsContent
    {
        private static string url = "https://tjzsprod.tsxcx.xyz/wxXcx/v1/wx/refreshToken";

        public TokenContent(Order order) : base(url, order)
        {
            SetHeader();
            BuildContent();
        }

        private void SetHeader()
        {
            AddHeader("Authorization", Order.RefreshToken);
        }

        private void BuildContent()
        {
            Content.AddOrUpdate("sys_id", 1001);
        }
    }
}
