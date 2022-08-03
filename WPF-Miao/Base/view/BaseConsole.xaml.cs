using CoreControl.LogConsole;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Base.view
{
    /// <summary>
    /// BaseConsole.xaml 的交互逻辑
    /// </summary>
    public partial class BaseConsole : UserControl
    {
        #region Properties

        private static readonly Type ControlType = typeof(BaseConsole);

        public LogPanel LogPanel
        {
            get { return (LogPanel)GetValue(LogPanelProperty); }
            set { SetValue(LogPanelProperty, value); }
        }

        public static readonly DependencyProperty LogPanelProperty =
            DependencyProperty.Register("LogPanel", typeof(LogPanel), ControlType);

        #endregion Properties

        #region Constructor

        public BaseConsole()
        {
            InitializeComponent();
            LogPanel = logPanelCtrl;
        }

        #endregion Constructor
    }
}
