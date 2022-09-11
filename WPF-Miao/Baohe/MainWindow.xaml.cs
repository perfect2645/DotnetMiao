using Baohe.viewModel;
using System;
using System.Windows;

namespace Baohe
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
                DataContext = new BaoheViewModel(baoheConsole.baseConsole.LogPanel);
            }
            catch (Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
            }
        }
    }
}
