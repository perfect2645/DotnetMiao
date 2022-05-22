using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace CoreControl.LogConsole
{
    /// <summary>
    /// LogPanel.xaml 的交互逻辑
    /// </summary>
    public partial class LogPanel : UserControl
    {
        private static readonly Type ControlType = typeof(LogPanel);

        public FlowDocument LogPanelDocument
        {
            get { return (FlowDocument)GetValue(LogPanelDocumentProperty); }
            set { SetValue(LogPanelDocumentProperty, value); }
        }

        public static readonly DependencyProperty LogPanelDocumentProperty =
            DependencyProperty.Register("LogPanelDocument", typeof(FlowDocument), ControlType);

        public LogPanel()
        {
            InitializeComponent();
        }

        private void logText_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
