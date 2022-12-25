using System;
using System.Windows;

namespace Redhouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                DataContext = new HysViewModel(mainConsole.baseConsole.LogPanel);
            }
            catch (Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
            }
        }
    }
}
