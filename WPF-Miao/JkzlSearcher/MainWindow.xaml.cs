using JkzlSearcher.viewModel;
using System;
using System.Windows;

namespace JkzlSearcher
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
                DataContext = new JkzlViewModel(mainConsole.baseConsole.LogPanel);
            }
            catch (Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
            }
        }
    }
}
