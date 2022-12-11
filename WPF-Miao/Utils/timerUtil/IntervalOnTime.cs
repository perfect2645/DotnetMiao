using System.Timers;

namespace Utils.timerUtil
{
    public class IntervalOnTime
    {
        #region Properties

        public System.Timers.Timer IntervalTimer { get; private set; }
        public ActionOnTime OnTimeTimer { get; private set; }
        public Action IntervalAction { get; private set; }
        public int Interval { get; set; } = 500;
        public string Name { get; private set; }

        #endregion Properties

        #region Constructor

        public IntervalOnTime(string name, int interval)
        {
            Interval = interval;
            InitIntervalTimer();
        }

        public IntervalOnTime(Action action, string name, int interval)
        {
            IntervalAction = action;
            Interval = interval;
            InitIntervalTimer();
        }

        public IntervalOnTime(Action action, string name, DateTime stratTime)
        {
            IntervalAction = action;

            InitOnTimeTimer(name, stratTime);

            InitIntervalTimer();
        }

        public IntervalOnTime(Action action, string name, DateTime stratTime, int interval)
        {
            IntervalAction = action;
            Interval = interval;

            InitOnTimeTimer(name, stratTime);

            InitIntervalTimer();
        }

        #endregion Constructor

        #region On Time Timer
        private void InitOnTimeTimer(string name, DateTime stratTime)
        {
            var onTimeInterval = (stratTime - DateTime.Now).TotalMilliseconds;
            onTimeInterval = Math.Ceiling(onTimeInterval);
            if (onTimeInterval < 0)
            {
                onTimeInterval = 200;
            }

            OnTimeTimer?.StopTimer();

            OnTimeTimer = new ActionOnTime(name, onTimeInterval)
            {
                ActionTime = stratTime,
                TargetAction = StartIntervalOntime,
            };
        }

        #endregion On Time Timer

        #region Interval Timer

        private void InitIntervalTimer()
        {
            StopInterval();
            IntervalTimer = new System.Timers.Timer();
            IntervalTimer.Enabled = false;
            IntervalTimer.Interval = Interval;

            IntervalTimer.Elapsed += new System.Timers.ElapsedEventHandler(IntervalTimer_Elapsed);
        }

        public void StartIntervalOntime()
        {
            IntervalTimer.Start();
        }

        public void StartIntervalOntime(Action action)
        {
            IntervalAction = action;
            IntervalTimer.Start();
        }

        private void IntervalTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            IntervalAction?.Invoke();
        }

        public void StopInterval()
        {
            IntervalTimer?.Stop();
            if (IntervalTimer != null)
            {
                IntervalTimer.Elapsed -= new System.Timers.ElapsedEventHandler(IntervalTimer_Elapsed);
            }
        }

        public void ResetTimer(DateTime stratTime, int interval)
        {
            Interval = interval;
            InitOnTimeTimer(Name, stratTime);
            InitIntervalTimer();
        }

        #endregion Interval Timer
    }
}
