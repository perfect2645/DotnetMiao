using HttpProcessor.Container;
using JkzlSearcher.session;

namespace JkzlSearcher.search
{
    internal class SearchController
    {
        private HosDeptController _deptController;

        public SearchController()
        {
            _deptController = HttpServiceController.GetService<HosDeptController>();
        }

        public void SearchByHospitalId()
        {
            var hospitalId = MainSession.GetNextHospitalId();
            _deptController.GetHosDeptAsync(hospitalId);
        }
    }
}
