using HttpProcessor.Content;
using hys020.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hys020.search
{
    internal class MiaoContent : HysBaseContent
    {
        public MiaoContent(string url) : base(url)
        {
            BuildHeader();
        }

        private void BuildHeader()
        {
        }
    }
}
