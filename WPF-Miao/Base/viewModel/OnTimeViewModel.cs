using Base.session;
using Base.viewmodel.status;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.ExceptionManager;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils.timerUtil;

namespace Base.viewModel
{
    public abstract class OnTimeViewModel : ViewModelBase
    {
        #region Properties

        private DateTime _startTime = DateTime.Now.AddSeconds(20);
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                if (_startTime != value)
                {
                    CheckSetStartTime(value);
                    NotifyUI(() => StartTime);
                    PrintLog($"设置开始时间:{_startTime}");
                }
            }
        }

        private void CheckSetStartTime(DateTime value)
        {
            var now = DateTime.Now;
            if (value < now)
            {
                var year = now.Year;
                var month = now.Month;
                var day = now.Day;
                var hour = value.Hour;
                var minute = value.Minute;
                var sec = value.Second;
                value = new DateTime(year, month, day, hour, minute, sec);
                value = value.AddDays(1);
            }
            _startTime = value;
        }

        private int _interval = 200;
        public int Interval
        {
            get { return _interval; }
            set
            {
                if (_interval != value)
                {
                    _interval = value;
                    NotifyUI(() => Interval);
                }
            }
        }

        public ICommand AutoRunCommand { get; set; }

        public IntervalOnTime AutoRunTimer { get; private set; }

        public ActionOnTime OnTimeTimer { get; private set; }

        #endregion Properties

        #region Constructor

        public OnTimeViewModel(LogPanel logPanel) : base(logPanel)
        {
            AutoRunCommand = new RelayCommand(StartAutoRun, CanAutoRun);
            MainSessionBase.MiaoStatus.Subscribe(OnMiaoStatusUpdate);
        }

        #endregion Constructor

        #region Ontime Action

        protected abstract void StartAutoRun();

        private bool CanAutoRun()
        {
            if (Interval > 20)
            {
                return true;
            }

            return false;
        }

        protected void StartIntervalTimer()
        {
            try
            {
                if (AutoRunTimer == null)
                {
                    AutoRunTimer = new IntervalOnTime(AutoRun, "开启自动模式", StartTime, Interval);
                    return;
                }

                AutoRunTimer.StopInterval();
                AutoRunTimer.ResetTimer(StartTime, Interval);
            }
            catch (Exception ex)
            {
                PrintLogEvent.Publish(this, "StartAutoRunTimer Failed");
                Logging.GLog.Logger.Error(ex);
            }
            finally
            {
                PrintLogEvent.Publish(this, "开启自动模式");
            }
        }

        protected void StopIntervalTimer()
        {
            AutoRunTimer?.StopInterval();
        }

        protected void StartOnTimeTimer()
        {
            try
            {
                if (OnTimeTimer == null)
                {
                    OnTimeTimer = new ActionOnTime("开启定时模式", Interval)
                    {
                        TargetAction = AutoRun,
                        ActionTime = StartTime,
                    };
                    return;
                }

                OnTimeTimer.StartTimer();
            }
            catch (Exception ex)
            {
                PrintLogEvent.Publish(this, "StartOnTimeTimer Failed");
                Logging.GLog.Logger.Error(ex);
            }
            finally
            {
                PrintLogEvent.Publish(this, "开启自动模式");
            }
        }

        protected void StopOnTimeTimer()
        {
            OnTimeTimer.StopTimer();
        }

        protected abstract void AutoRun();

        #endregion Ontime Action

        #region Status Control

        protected virtual void OnMiaoStatusUpdate(object? sender, StatusEventArgs e)
        {
            var currentStatus = e.CurrentStatus;

            switch (currentStatus)
            {
                case MiaoProgress.Init: OnStatusInit(); break;
                case MiaoProgress.ReadyForSearch: OnReadyForSearch(); break;
                case MiaoProgress.Searching: OnSearching(); break;
                case MiaoProgress.Searchend: OnSearchend(); break;
                case MiaoProgress.MiaoGet: OnMiaoGet(e); break;
            }
        }


        protected virtual void OnStatusInit()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    OnInitAsync();
                }
                catch (HttpException ex)
                {
                    Log(ex);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            });
        }

        protected abstract void OnInitAsync();


        protected virtual void OnReadyForSearch()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    OnReadyForSearchAsync();
                }
                catch (HttpException ex)
                {
                    Log(ex);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            });
        }

        protected abstract void OnReadyForSearchAsync();

        protected virtual void OnSearchend()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    OnSearchendAsync();
                }
                catch (HttpException ex)
                {
                    Log(ex);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            });
        }

        protected abstract void OnSearchendAsync();

        protected virtual void OnSearching()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    OnSearchingAsync();
                }
                catch (HttpException ex)
                {
                    Log(ex);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            });
        }

        protected abstract void OnSearchingAsync();

        private void OnMiaoGet(StatusEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    OnMiaoGetAsync(e.Data);
                }
                catch (HttpException ex)
                {
                    Log(ex);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            });
        }

        protected virtual void OnMiaoGetAsync(object data)
        {
        }

        #endregion Status Control
    }
}
