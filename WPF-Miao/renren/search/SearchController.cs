using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Container;
using renren.appointment;
using renren.search.hospital;
using renren.search.patient;
using renren.session;
using System.Net.Http;
using System.Threading.Tasks;
using Utils.timerUtil;

namespace renren.search
{
    internal class SearchController : HttpClientBase
    {
        private IntervalOnTime SearchInterval { get; set; }

        public SearchController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task SearchAsync()
        {
            await Task.Factory.StartNew(() => Search());
            //SearchInterval.StopInterval();

            //await Yuyue();
        }

        private async Task Yuyue()
        {
            var appointController = HttpServiceController.GetService<AppointController>();
            appointController.AppointAsync();
        }

        private async Task Search()
        {
            var patientController = HttpServiceController.GetService<PatientController>();
            var patientTask = patientController.GetUserInfoAsync();

            var hosptialController = HttpServiceController.GetService<HospitalTeamsController>();
            var hospitalTask = hosptialController.GetHospitalTeamsAsync();

            await Task.WhenAll(patientTask, hospitalTask);
            MainSession.SetStatus(MiaoProgress.Searchend);
        }
    }
}
