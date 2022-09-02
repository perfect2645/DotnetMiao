using System.Timers;

namespace Utils.timerUtil
{
    public class ActionOnTime
    {
        public System.Timers.Timer OnTimeTimer { get; private set; }

        public List<DateTime> ActionTimeList { get; set; }
        public DateTime ActionTime { get; set; }
        public Action TargetAction { get; set; }
        public string Name { get; }
        public ActionOnTime(string name)
        {
            Name = name;
            OnTimeTimer = new System.Timers.Timer();
            OnTimeTimer.Enabled = true;
            OnTimeTimer.Interval = 30000;
            OnTimeTimer.Start();

            OnTimeTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (e.SignalTime > ActionTime)
            {
                Logging.GLog.Logger.Info($"{Name} Action tick");
                TargetAction?.Invoke();
            }
        }

        #region Public

        public void StartTimer()
        {
            OnTimeTimer.Start();
        }

        public void StopTimer()
        {
            OnTimeTimer.Stop();
        }

        #endregion Public
    }
}
