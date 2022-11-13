using chutian.login;
using HttpProcessor.Container;
using System;
using System.Threading.Tasks;

namespace chutian.search
{
    internal class SearchController
    {

        private DoctorInfoController _doctorInfoController;

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
