using Base.session;
using Base.viewModel;
using System.Collections.Generic;

namespace Baohe.session
{
    internal class MiaoSession : ISession
    {
        public string Key { get; set; }
        public ISessionItem SessionItem { get; set; }
    }
}
