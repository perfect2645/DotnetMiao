using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Utils.timerUtil
{
    public class IntervalOnTime
    {
        public System.Timers.Timer IntervalTimer { get; private set; }
        public ActionOnTime OnTimeTimer { get; private set; }
        public Action IntervalAction { get; private set; }
        public int Interval { get; set; } = 100;
        public string Name { get; private set; }

        public IntervalOnTime(Action action, string name, DateTime stratTime)
        {
            IntervalAction = action;

            InitOnTimeTimer(name, stratTime);

            InitIntervalTimer();
        }

        #region On Time Timer
        private void InitOnTimeTimer(string name, DateTime stratTime)
        {
            var onTimeInterval = (stratTime - DateTime.Now).TotalMilliseconds;
            onTimeInterval = Math.Ceiling(onTimeInterval);
            if (onTimeInterval < 0)
            {
                return;
            }

            OnTimeTimer = new ActionOnTime(name, onTimeInterval)
            {
                ActionTime = stratTime,
                TargetAction = StartIntervalOntime,
            };
        }
        private void StartIntervalOntime()
        {
            IntervalTimer.Start();
        }

        #endregion On Time Timer

        #region Interval Timer

        private void InitIntervalTimer()
        {
            IntervalTimer = new System.Timers.Timer();
            IntervalTimer.Enabled = false;
            IntervalTimer.Interval = Interval;

            IntervalTimer.Elapsed += new System.Timers.ElapsedEventHandler(IntervalTimer_Elapsed);
        }

        private void IntervalTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            IntervalAction?.Invoke();
        }

        public void StopInterval()
        {
            IntervalTimer.Stop();
        }

        #endregion Interval Timer
    }
}
