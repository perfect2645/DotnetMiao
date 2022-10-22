using Base.session;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.ExceptionManager;
using renren.viewmodel;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils.timerUtil;

namespace Base.viewModel
{
    public abstract class OnTimeViewModel : ViewModelBase
    {
        #region Properties

        private DateTime _startTime = DateTime.Now.AddMinutes(2);
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                if (_startTime != value)
                {
                    _startTime = value;
                    NotifyUI(() => StartTime);
                }
            }
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

        #endregion Properties

        #region Constructor

        public OnTimeViewModel(LogPanel logPanel) : base(logPanel)
        {
            AutoRunCommand = new RelayCommand(StartAutoRunTimer, CanAutoRun);
            MainSessionBase.MiaoStatus.Subscribe(OnMiaoStatusUpdate);
        }

        #endregion Constructor

        #region Ontime Action

        private bool CanAutoRun()
        {
            if (Interval > 20)
            {
                return true;
            }

            return false;
        }

        protected void StartAutoRunTimer()
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

        protected void StopAutoRunTimer()
        {
            AutoRunTimer.StopInterval();
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
                case MiaoProgress.GettingUser: OnGettingUser(); break;
                case MiaoProgress.UserGet: OnUserGet(); break;
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


        protected virtual void OnGettingUser()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    OnGettingUserAsync();
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

        protected abstract void OnGettingUserAsync();

        protected virtual void OnUserGet()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    OnUserGetAsync();
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

        protected abstract void OnUserGetAsync();

        #endregion Status Control
    }
}
