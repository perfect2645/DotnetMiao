using System;

namespace WPF_Miao.Platform.yunnan.viewModel
{
    internal interface ISessionItem
    {
        string UserName { get; set; }
        string Cookie { get; set; }
        string Referer { get; set; }
        int Tel { get; set; }
        Action GetUserSessionAction { get; set; }
    }
}
