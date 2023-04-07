using Base.session;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.ExceptionManager;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using Utils.datetime;
using Utils.timerUtil;
using Timer = System.Timers.Timer;

namespace Base.search
{
    public enum SearchStatus
    {
        Start,
        WaterGet,
        NumbersGet,
    }
    public class SearchController : HttpClientBase
    {
        private SearchStatus SearchStatus { get; set; }

        public Timer AutoRunTimer { get; set; }

        public ActionOnTime StartWaterSearchTimer { get; set; }

        private readonly object OrderLock = new object();

        public SearchController(HttpClient httpClient) : base(httpClient)
        {
            InitAuoRunTimer();
        }

        private void InitAuoRunTimer()
        {
            AutoRunTimer = new Timer();
            AutoRunTimer.Enabled = false;
            AutoRunTimer.Interval = 3000;

            AutoRunTimer.AutoReset = true;

            AutoRunTimer.Elapsed += new ElapsedEventHandler(AutoRunTimer_ElapsedAsync);

            //var date = new DateTime(2022, 9, 2, 19, 59, 56);
            var date = DateTime.Now.AddMinutes(1);
            StartWaterSearchTimer = new ActionOnTime("开始查miao", 500)
            {
                ActionTime = date,
                TargetAction = () =>
                {
                    AutoRunTimer.Start();
                },
            };
        }

        public async Task SearchAllAsync(ISessionItem sessionItem)
        {
            await SearchUserInfoAsync(sessionItem);

            await SearchMiaoInfo();

            //BuildMiaoOrder();
        }

        #region AutoRun

        public async Task AutoSearchAsync(ISessionItem sessionItem)
        {
            await SearchUserInfoAsync(sessionItem);

            //await SearchMiaoInfo();

        }

        private async void AutoRunTimer_ElapsedAsync(object? sender, ElapsedEventArgs e)
        {
            try
            {
                if (SearchStatus == SearchStatus.NumbersGet)
                {
                    ISessionContainer.PrintLogEvent.Publish(this, $"AutoRunTimer stopped start. SearchStatus={SearchStatus}");
                    AutoRunTimer.Stop();

                    return;
                }
                await SearchMiaoInfo();
            }
            catch (HttpException ex)
            {
                ISessionContainer.PrintLogEvent.Publish(this, ex.Message);
            }
            catch (Exception ex)
            {
                ISessionContainer.PrintLogEvent.Publish(this, ex.StackTrace ?? ex.Message);
            }
        }

        #endregion AutoRun



        private async Task SearchMiaoInfo()
        {
            if (SearchStatus == SearchStatus.Start)
            {

            }

            if (SearchStatus == SearchStatus.WaterGet)
            {

            }
        }

        private async Task SearchUserInfoAsync(ISessionItem sessionItem)
        {
            try
            {

            }
            catch (HttpException ex)
            {
                ISessionContainer.PrintLogEvent.Publish(this, ex.Message);
            }
            catch (Exception ex)
            {
                ISessionContainer.PrintLogEvent.Publish(this, ex.StackTrace ?? ex.Message);
            }
        }
    }
}
