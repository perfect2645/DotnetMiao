using HttpProcessor.Client;
using HttpProcessor.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Miao.Platform.shanxi.Clients.Appointment
{
    internal class AppointmentContent : HttpMessageContent
    {
        public AppointmentContent(string requestUrl) : base(requestUrl)
        {
        }
    }
}
