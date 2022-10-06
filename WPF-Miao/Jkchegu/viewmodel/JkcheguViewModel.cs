using Base.Events;
using Base.model;
using Base.viewModel;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Jkchegu.appointment;
using Jkchegu.search;
using Jkchegu.session;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;

namespace Jkchegu.viewmodel
{
    internal class JkcheguViewModel : ViewModelBase
    {
        #region Properties

        public ICommand SearchCommand { get; set; }
        public ICommand AppointCommand { get; set; }

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
                JkSession.MiaoSession.AddOrUpdate("Date", value.Value);
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
                JkSession.MiaoSession.AddOrUpdate("Time", value.Value);
                NotifyUI(() => SelectedTime);
            }
        }

        #endregion Properties

        #region Constructor

        public JkcheguViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitStaticData();
            InitCommands();
            JkSession.PrintLogEvent = PrintLogEvent;
        }

        private void InitStaticData()
        {
            DateList = new List<DspVal>
            {
                new DspVal("2022-10-10"),
                new DspVal("2022-10-11"),
                new DspVal("2022-10-12"),
                new DspVal("2022-10-13"),
                new DspVal("2022-10-14"),
            };

            TimeList = new List<DspVal>
            {
                new DspVal("8:00-8:30", "DATE1_COUNT"),
                new DspVal("8:30-9:00", "DATE2_COUNT"),
                new DspVal("9:00-9:30", "DATE3_COUNT"),
                new DspVal("9:30-10:00", "DATE4_COUNT"),
                new DspVal("10:00-10:30", "DATE5_COUNT"),
                new DspVal("10:30-11:00", "DATE6_COUNT"),
            };
        }

        private void InitCommands()
        {
            SearchCommand = new AsyncRelayCommand(ExecuteSearchAsync);
            AppointCommand = new RelayCommand(ExecuteAppoint);

            SessionEvents.Instance.Subscribe(LogSession);
        }

        #endregion Constructor

        #region Search

        private async Task ExecuteSearchAsync()
        {
            try
            {
                JkSession.Cookie = Cookie;
                var searchController = HttpServiceController.GetService<SearchController>();
                await searchController.SearchAsync();
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

        private void ExecuteAppoint()
        {
            try
            {
                JkSession.Cookie = Cookie;
                var appointController = HttpServiceController.GetService<AppointController>();
                appointController.AppointAsync();
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

        #region Session

        private void LogSession(object? sender, SesstionEventArgs args)
        {

        }

        #endregion Session
    }
}
