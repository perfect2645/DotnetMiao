using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;

namespace renren.viewmodel
{
    public enum MiaoProgress
    {
        Init,
        GettingUser,
        UserGet,
        GettingMiao,
        MiaoGet,
        AppointStart,
        AppointEnd,
        Error = 400
    }
    public class MiaoStatus : NotifyChanged
    {
        #region Properties

        private MiaoProgress _miaoProgress;
        public MiaoProgress MiaoProgress
        {
            get { return _miaoProgress; }
            set
            {
                if (_miaoProgress == value)
                {
                    return;
                }
                _miaoProgress = value;
                NotifyUI(() => MiaoProgress);
                OnMiaoProgressUpdate();
            }
        }

        private string _statusDesc;
        public string StatusDesc
        {
            get { return _statusDesc; }
            set
            {
                _statusDesc = value;
                NotifyUI(() => StatusDesc);
            }
        }

        #endregion Properties

        #region Constructor

        public MiaoStatus()
        {

        }

        #endregion Constructor

        #region Progress Event

        public event EventHandler<StatusEventArgs> ProgressEvent;

        public void Subscribe(EventHandler<StatusEventArgs> handler)
        {
            ProgressEvent += handler;
        }

        public void Publish(object? sender, StatusEventArgs e)
        {
            ProgressEvent?.Invoke(sender, e);
        }

        private void OnMiaoProgressUpdate()
        {
            Task.Factory.StartNew(() =>
            {
                Publish(this, new StatusEventArgs
                {
                    CurrentStatus = MiaoProgress,
                });
            });
        }

        #endregion Progress Event

        #region Print status

        public void UpdateStatusDesc(MiaoProgress oldValue, MiaoProgress newValue)
        {
            StatusDesc = $"{newValue} <- {oldValue} - {DateTimeUtil.GetTime()}";
        }

        #endregion Print status
    }

    public class StatusEventArgs
    {
        public MiaoProgress CurrentStatus { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }
}
