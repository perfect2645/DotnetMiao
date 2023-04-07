using HttpProcessor.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hys020.appointment.Yuyue
{
    internal class YuyueContent : HttpStringContent
    {
        public YuyueContent(string url) : base(url)
        {
        }
    }
}
