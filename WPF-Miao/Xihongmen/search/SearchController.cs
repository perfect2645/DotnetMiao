using Xihongmen.login;
using HttpProcessor.Container;
using System;
using System.Threading.Tasks;

namespace Xihongmen.search
{
    internal class SearchController
    {

        private User _doctorInfoController;

        public SearchController()
        {
            _doctorInfoController = HttpServiceController.GetService<DoctorInfoController>();
        }

        public void SearchAsync()
        {
            _doctorInfoController.SearchMiaoAsync();
        }
    }
}
