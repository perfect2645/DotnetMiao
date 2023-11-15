using HttpProcessor.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.datetime;
using Zhuzher.Common;

namespace Zhuzher.Score
{
    internal class CollectScoreContent : ZhuzherContent
    {
        private const string _url = "https://chaos.4009515151.com/market/api/notice/scene";
        public CollectScoreContent() : base(_url)
        {
            BuildContent();
        }



        private void BuildContent()
        {
            //AddContent("matchParam", @"https:\/\/uiis.4009515151.com\/fg_activity\/template?id=2180");
            var nowTimestapm = DateTimeUtil.GetTimeStamp();
            AddContent("requestId", nowTimestapm);
            AddContent("value", string.Empty);
        }
    }
}
