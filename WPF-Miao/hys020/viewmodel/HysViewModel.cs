using Base.Events;
using Base.model;
using Base.viewmodel.status;
using Base.viewModel;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using hys020.appointment;
using hys020.search;
using hys020.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.stringBuilder;

namespace hys020.viewmodel
{
    internal class HysViewModel : OnTimeViewModel
    {
        #region Properties

        public ICommand SearchCommand { get; set; }
        public ICommand AppointCommand { get; set; }
        public ICommand YzmCommand { get; set; }

        private List<DspVal> _dateList;
        public List<DspVal> DateList
        {
            get { return _dateList; }
            set
            {
                _dateList = value;
                NotifyUI(() => DateList);
            }
        }

        private DspVal _selectedDate;
        public DspVal SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                //MainSession.MiaoSession.AddOrUpdate("Date", value.Value);
                NotifyUI(() => SelectedDate);
            }
        }

        private List<DspVal> _timeList;
        public List<DspVal> TimeList
        {
            get { return _timeList; }
            set
            {
                _timeList = value;
                NotifyUI(() => TimeList);
            }
        }

        private DspVal _selectedTime;
        public DspVal SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                _selectedTime = value;
                //MainSession.MiaoSession.AddOrUpdate("Time", value.Value);
                NotifyUI(() => SelectedTime);
            }
        }

        private readonly object OrderLock = new object();

        private SearchController _searchController;

        #endregion Properties

        #region Constructor

        public HysViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            InitControllers();
        }

        private void InitControllers()
        {
            _searchController = HttpServiceController.GetService<SearchController>();
        }

        private void InitStaticData()
        {
            //MainSession.MiaoSession.AddOrUpdate("StartTime", new DateTime(2022, 10, 7, 8, 57, 0));
        }

        private void InitCommands()
        {
            MainSession.PrintLogEvent = PrintLogEvent;

            SearchCommand = new RelayCommand(OnSearchClick);
            AppointCommand = new RelayCommand(ExecuteAppointAsync);

            MainSession.AppointEvent.Subscribe(OnAppointment);
        }

        #endregion Constructor

        #region Status Control

        protected override void OnInitAsync()
        {

        }

        protected override void OnReadyForSearchAsync()
        {
        }

        protected override void OnSearchingAsync()
        {

        }

        protected override void OnSearchendAsync()
        {

        }

        #endregion Status Control

        #region Search

        private void OnSearchClick()
        {
            Task.Factory.StartNew(async () =>
            {
                await SearchAsync();
                if (MainSession.MiaoStatus.MiaoProgress != MiaoProgress.GettingMiao)
                {
                    MainSession.SetStatus(MiaoProgress.GettingMiao);
                }
                var miaoSchedule = HttpServiceController.GetService<SearchController>();
                await miaoSchedule.SearchAsync();
            });
        }

        private async Task SearchAsync()
        {
            if (MainSession.MiaoStatus.MiaoProgress == MiaoProgress.Init)
            {
                PrintLogEvent.Publish(this, "请补全必须的信息");
                return;
            }

            try
            {
                MainSession.SetStatus(MiaoProgress.Searching);
                await _searchController.SearchAsync();
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        #endregion Search

        #region Appoint

        private void OnAppointment(object? sender, AppointEventArgs e)
        {
            lock (OrderLock)
            {
                var orderList = e.OrderList;
                ConsumeOrdersAsync(orderList);
            }
        }

        private void ConsumeOrdersAsync(List<Order> orderList)
        {
            try
            {
                var appointController = HttpServiceController.GetService<AppointController>();
                appointController.Yuyue(orderList);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private async void ExecuteAppointAsync()
        {
            try
            {
                MainSession.Cookie = Cookie;
                var appointController = HttpServiceController.GetService<AppointController>();
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        #endregion Appoint

        #region AutoRun

        protected override async void StartAutoRun()
        {
            await SearchAsync();
            StartAutoRunTimer();
        }

        protected override void AutoRun()
        {
            Task.Factory.StartNew(async () =>
            {
                PrintLogEvent.Publish(this, "开始了");
                if (MainSession.MiaoStatus.MiaoProgress != MiaoProgress.GettingMiao)
                {
                    MainSession.SetStatus(MiaoProgress.GettingMiao);
                }
                var miaoSchedule = HttpServiceController.GetService<SearchController>();
                await miaoSchedule.SearchAsync();
            });
        }

        #endregion AutoRun
    }
}
