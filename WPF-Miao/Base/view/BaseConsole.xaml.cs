using CoreControl.LogConsole;
using System;
using System.Windows;
using System.Windows.Automation;
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

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), ControlType, new PropertyMetadata("请选择医院"));

        public LogPanel LogPanel
        {
            get { return (LogPanel)GetValue(LogPanelProperty); }
            set { SetValue(LogPanelProperty, value); }
        }

        public static readonly DependencyProperty LogPanelProperty =
            DependencyProperty.Register("LogPanel", typeof(LogPanel), ControlType);

        public StackPanel ButtonArea
        {
            get { return (StackPanel)GetValue(ButtonAreaProperty); }
            set { SetValue(ButtonAreaProperty, value); }
        }

        public static readonly DependencyProperty ButtonAreaProperty =
            DependencyProperty.Register("ButtonArea", typeof(StackPanel), ControlType);

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
