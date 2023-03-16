using Baohe.appointment;
using Baohe.constants;
using Baohe.search.ArrangeWater;
using Baohe.search.cookie;
using Baohe.search.doctor;
using Baohe.search.numbers;
using Baohe.search.user;
using Baohe.session;
using Baohe.verification;
using Base.Events;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Utils;
using Utils.datetime;
using Utils.number;
using Utils.stringBuilder;
using Utils.timerUtil;
using Timer = System.Timers.Timer;

namespace Baohe.search
{
    internal enum SearchStatus
    {
        Start,
        WaterGet,
        NumbersGet,
    }
    internal class SearchController : HttpClientBase
    {
        private SearchStatus SearchStatus { get; set; }

        public Timer AutoRunTimer { get; set; }

        public ActionOnTime StartWaterSearchTimer { get; set; }

        public string UserName { get; set; }
        public string UserPhone { get; set; }

        private readonly object OrderLock = new object();
        private readonly object YzmLock = new object();

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

        }

        public void SetTimer()
        {
            var startTime = MainSession.GetStartTime();
            startTime = startTime.AddSeconds(-2);

            //var date = new DateTime(2022, 9, 15, 21, 59, 58);
            //var date = DateTime.Now.AddMinutes(1);
            StartWaterSearchTimer = new ActionOnTime("开始查miao", 500)
            {
                ActionTime = startTime,
                TargetAction = () =>
                {
                    AutoRunTimer.Start();
                },
            };
        }

        public void StopTimer()
        {
            AutoRunTimer.Stop();
        }

        internal async Task SearchAllAsync(string userName)
        {
            await SearchUserInfo(userName);

            await SearchMiaoInfo();

            //BuildMiaoOrder();
        }

        #region AutoRun

        internal async Task AutoSearchAsync(string userName)
        {
            await SearchUserInfo(userName);

            //await SearchMiaoInfo();

        }

        private async void AutoRunTimer_ElapsedAsync(object? sender, ElapsedEventArgs e)
        {
            try
            {
                if (SearchStatus == SearchStatus.NumbersGet)
                {
                    MainSession.PrintLogEvent.Publish(this, $"AutoRunTimer stopped start. SearchStatus={SearchStatus}");
                    AutoRunTimer.Stop();

                    return;
                }
                await SearchMiaoInfo();
            }
            catch (HttpException ex)
            {
                MainSession.PrintLogEvent.Publish(this, ex.Message);
            }
            catch (Exception ex)
            {
                StopTimer();
                MainSession.PrintLogEvent.Publish(this, ex.StackTrace ?? ex.Message);
            }
        }

        #endregion AutoRun



        private async Task SearchMiaoInfo()
        {
            if (SearchStatus == SearchStatus.Start)
            {
                MainSession.DefaultWater = new Dictionary<string, object>();
                var arrangeWater = HttpServiceController.GetService<ArrangeWaterController>();
                var isWaterGet = await arrangeWater.GetArrangeWaterAsync();
                if (isWaterGet)
                {
                    SearchStatus = SearchStatus.WaterGet;
                }
            }

            if (SearchStatus == SearchStatus.WaterGet)
            {
                var yzmController = HttpServiceController.GetService<YzmController>();

                var arrangeWaterList = SessionBuilder.GetAvailableArrangeWater();

                var index = 0;
                if (arrangeWaterList.Count > 0)
                {
                    index = NumberUtil.IntRandom(0, arrangeWaterList.Count - 1);
                }
                MainSession.DefaultWater = arrangeWaterList[index]!;

                if (!MainSession.IsYzmSent)
                {
                    var isYzmSent = await yzmController.SendYzmAsync(UserName, UserPhone, MainSession.DefaultWater["ArrangeID"].NotNullString());
                    if (isYzmSent)
                    {
                        MainSession.IsYzmSent = true;
                    }
                }

                if (MainSession.IsYzmSent)
                {
                    var appointNumbers = HttpServiceController.GetService<AppointNumbersController>();
                    var isNumbersGet = await appointNumbers.GetNumbersAsync(MainSession.DefaultWater);

                    MainSession.PrintLogEvent.Publish(this, $"isNumbersGet={isNumbersGet}");

                    lock (OrderLock)
                    {
                        if (isNumbersGet && SearchStatus == SearchStatus.WaterGet && MainSession.IsYzmChecked)
                        {
                            SearchStatus = SearchStatus.NumbersGet;

                            lock (OrderLock)
                            {
                                BuildMiaoOrder();
                            }
                        }
                    }
                }
            }
        }

        private async Task SearchUserInfo(string userName)
        {
            try
            {
                var authController = HttpServiceController.GetService<CookieController>();
                authController.GetCookieAdvance(MainSession.Cookie);

                var userInfoContr = HttpServiceController.GetService<UserInfoController>();
                await userInfoContr.GetUserInfoAsync();

                var defaultMember = SessionBuilder.GetDefaultMember(userName);
                var phone = defaultMember.GetString(Constant.Phone);
                MainSession.UpdateUI("phone", phone);

                var doctorContr = HttpServiceController.GetService<DoctorController>();
                await doctorContr.GetDoctorListAsync();

                //var liudiao = HttpServiceController.GetService<LiudiaoController>();
                //await liudiao.LiudiaoAsync(sessionItem);
                var authContr = HttpServiceController.GetService<AuthController>();
                await authContr.CheckAuthAsync(userName);

                BuildMemberOrder(defaultMember);
            }
            catch (HttpException ex)
            {
                StopTimer();
                MainSession.PrintLogEvent.Publish(this, ex.Message);
            }
            catch (Exception ex)
            {
                StopTimer();
                MainSession.PrintLogEvent.Publish(this, ex.StackTrace ?? ex.Message);
            }
        }

        private void BuildMemberOrder(Dictionary<string, object> defaultMember)
        {
            if (!defaultMember.HasItem())
            {
                return;
            }

            MainSession.OrderSession.Clear();

            for(var i = 0; i < 5; i++)
            {
                var order = new Order(defaultMember, i);
                MainSession.OrderSession.AddOrder(order);
            }
        }

        private void BuildMiaoOrder()
        {
            var orders = MainSession.OrderSession.GetOrders();
            var numberCount = (MainSession.MiaoSession["Numbers"] as IList).Count;

            var appContr = HttpServiceController.GetService<AppointmentController>();
            if (orders.Count > numberCount)
            {
                orders = orders.Take(numberCount).ToList();
            }

            foreach (var order in orders)
            {
                order.FillContent(MainSession.MiaoSession);
                Thread.Sleep(3000);
            }
        }
    }
}
