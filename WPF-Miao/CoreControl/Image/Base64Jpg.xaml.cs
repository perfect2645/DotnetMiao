using CoreControl.DateControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoreControl.Image
{
    /// <summary>
    /// Base64Jpg.xaml 的交互逻辑
    /// </summary>
    public partial class Base64Jpg : UserControl
    {
        #region Properties

        private static readonly Type ControlType = typeof(Base64Jpg);


        public string ImgSource
        {
            get { return (string)GetValue(ImgSourceProperty); }
            set { SetValue(ImgSourceProperty, value); }
        }

        public static readonly DependencyProperty ImgSourceProperty =
            DependencyProperty.Register("ImgSource", typeof(string), ControlType);

        public ICommand RefreshCommand
        {
            get { return (ICommand)GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }

        public static readonly DependencyProperty RefreshCommandProperty =
            DependencyProperty.Register("RefreshCommand", typeof(ICommand), ControlType);

        #endregion Properties

        #region Constructor

        public Base64Jpg()
        {
            InitializeComponent();
        }

        #endregion Constructor
    }
}
