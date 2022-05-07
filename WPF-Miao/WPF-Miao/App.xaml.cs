using HttpProcessor.Container;
using Logging;
using System;
using System.Configuration;
using System.Windows;
using WPF_Miao.Properties;

namespace WPF_Miao
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            GLog.Logger.Info("***************** Application Start *****************");
            base.OnStartup(e);
            InitAppDoman();
        }

        private void InitAppDoman()
        {
            var logPath = Settings.Default.LogPath;
            AppDomain.CurrentDomain.SetData("LogPath", logPath);
        }
    }
}
