using System;
using System.Windows;
using Tianhe.viewmodel;

namespace Tianhe
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
                DataContext = new TianheViewModel(mainConsole.baseConsole.LogPanel);
            }
            catch (Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
            }
        }
    }
}
