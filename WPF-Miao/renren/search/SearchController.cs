using HttpProcessor.Client;
using HttpProcessor.Container;
using renren.appointment;
using renren.search.patient;
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

        private async void Search()
        {
            var patientController = HttpServiceController.GetService<PatientController>();
            var patientTask = patientController.GetUserInfoAsync();

            var hosptialController = HttpServiceController.GetService<PatientController>();
            var hospitalTask =  
        }
    }
}
