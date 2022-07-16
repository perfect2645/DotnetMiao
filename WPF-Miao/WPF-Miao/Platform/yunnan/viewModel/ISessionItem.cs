using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Miao.Platform.yunnan.viewModel
{
    internal interface ISessionItem
    {
        string Cookie { get; set; }
        string Referer { get; set; }
    }
}
