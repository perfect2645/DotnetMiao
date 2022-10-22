using Base.viewModel.hospital;
using CoreControl.LogConsole;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Base.view
{
    /// <summary>
    /// OnTimeConsole.xaml 的交互逻辑
    /// </summary>
    public partial class OnTimeConsole : UserControl
    {
        #region Properties

        private static readonly Type ControlType = typeof(OnTimeConsole);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), ControlType, new PropertyMetadata("请选择医院-科室"));

        public List<HospitalDept> HospitalDeptList
        {
            get { return (List<HospitalDept>)GetValue(HospitalDeptListProperty); }
            set { SetValue(HospitalDeptListProperty, value); }
        }

        public static readonly DependencyProperty HospitalDeptListProperty =
            DependencyProperty.Register("HospitalDeptList", typeof(List<HospitalDept>), ControlType);

        public HospitalDept SelectedHospitalDept
        {
            get { return (HospitalDept)GetValue(SelectedHospitalDeptProperty); }
            set { SetValue(SelectedHospitalDeptProperty, value); }
        }

        public static readonly DependencyProperty SelectedHospitalDeptProperty =
            DependencyProperty.Register("SelectedHospitalDept", typeof(HospitalDept), ControlType, new PropertyMetadata(SelectedHospitalDeptChanged));

        public LogPanel LogPanel
        {
            get { return (LogPanel)GetValue(LogPanelProperty); }
            set { SetValue(LogPanelProperty, value); }
        }

        public static readonly DependencyProperty LogPanelProperty =
            DependencyProperty.Register("LogPanel", typeof(LogPanel), ControlType);

        public StackPanel DynamicArea
        {
            get { return (StackPanel)GetValue(DynamicAreaProperty); }
            set { SetValue(DynamicAreaProperty, value); }
        }

        public static readonly DependencyProperty DynamicAreaProperty =
            DependencyProperty.Register("DynamicArea", typeof(StackPanel), ControlType);

        public Visibility IsCookieVisible
        {
            get { return (Visibility)GetValue(IsCookieVisibleProperty); }
            set { SetValue(IsCookieVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsCookieVisibleProperty =
            DependencyProperty.Register("IsCookieVisible", typeof(Visibility), ControlType, new PropertyMetadata(Visibility.Visible));

        #endregion Properties

        #region Constructor

        public OnTimeConsole()
        {
            InitializeComponent();
            LogPanel = logPanelCtrl;
        }

        #endregion Constructor

        #region events

        private static void SelectedHospitalDeptChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as OnTimeConsole;
            ctrl.Title = ctrl.SelectedHospitalDept?.Display;
        }
        #endregion events
    }
}
