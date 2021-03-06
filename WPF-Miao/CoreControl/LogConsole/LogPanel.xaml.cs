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

        private static readonly object _lock = new object();

        public Action<string> WriteLogAction { get; private set; }
        public Func<string> GetLogAction { get; private set; }

        public LogPanel()
        {
            InitializeComponent();
            WriteLogAction = new Action<string>(WriteLine);
            GetLogAction = new Func<string>(GetRichText);
        }

        private void logText_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        #region Write
        private void WriteLine(string text)
        {
            lock(_lock)
            {
                var paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run(text));
                document.Blocks.Add(paragraph);
            }
        }

        #endregion Write

        #region Read

        public string GetRichText()
        {
            if (logText.Document?.ContentStart == null)
            {
                return string.Empty;
            }
            TextRange textRange = new TextRange(logText.Document.ContentStart, logText.Document.ContentEnd);
            return textRange?.Text ?? string.Empty;
        }

        #endregion Read
    }
}
