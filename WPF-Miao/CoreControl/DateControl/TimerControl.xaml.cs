using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CoreControl.DateControl
{
    /// <summary>
    /// TimerControl.xaml 的交互逻辑
    /// </summary>
    public partial class TimerControl : UserControl
    {
        #region Properties

        private static readonly Type ControlType = typeof(TimerControl);

        public DateTime StartTime
        {
            get { return (DateTime)GetValue(StartTimeProperty); }
            set { SetValue(StartTimeProperty, value); }
        }

        public static readonly DependencyProperty StartTimeProperty =
            DependencyProperty.Register("StartTime", typeof(DateTime), ControlType, new PropertyMetadata(DateTime.Now.AddMinutes(2)));

        public int Interval
        {
            get { return (int)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(int), ControlType, new PropertyMetadata(200));

        public ICommand StartCommand
        {
            get { return (ICommand)GetValue(StartCommandProperty); }
            set { SetValue(StartCommandProperty, value); }
        }

        public static readonly DependencyProperty StartCommandProperty =
            DependencyProperty.Register("StartCommand", typeof(ICommand), ControlType);

        public string CommandName
        {
            get { return (string)GetValue(CommandNameProperty); }
            set { SetValue(CommandNameProperty, value); }
        }

        public static readonly DependencyProperty CommandNameProperty =
            DependencyProperty.Register("CommandName", typeof(string), ControlType, new PropertyMetadata("启动"));

        public ICommand StopCommand
        {
            get { return (ICommand)GetValue(StopCommandProperty); }
            set { SetValue(StopCommandProperty, value); }
        }

        public static readonly DependencyProperty StopCommandProperty =
            DependencyProperty.Register("StopCommand", typeof(ICommand), ControlType);

        public string CommandName2
        {
            get { return (string)GetValue(CommandName2Property); }
            set { SetValue(CommandName2Property, value); }
        }

        public static readonly DependencyProperty CommandName2Property =
            DependencyProperty.Register("CommandName2", typeof(string), ControlType, new PropertyMetadata("停止"));

        #endregion Properties

        #region Constructor

        public TimerControl()
        {
            InitializeComponent();
        }

        #endregion Constructor
    }
}
