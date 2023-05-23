using System;
using System.Windows;
using Shangyu.viewmodel;


namespace Shangyu
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
                DataContext = new ShangyuViewModel(mainConsole.baseConsole.LogPanel);
            }
            catch (Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
            }
        }
    }
}
