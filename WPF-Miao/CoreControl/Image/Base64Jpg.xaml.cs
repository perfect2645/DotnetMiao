using CoreControl.DateControl;
using NLog.Time;
using System;
using System.Collections.Generic;
using System.IO;
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
using Utils.Img;

namespace CoreControl.Image
{
    /// <summary>
    /// Base64Jpg.xaml 的交互逻辑
    /// </summary>
    public partial class Base64Jpg : UserControl
    {
        #region Properties

        private static readonly Type ControlType = typeof(Base64Jpg);


        public ImageSource ImgSource
        {
            get { return (ImageSource)GetValue(ImgSourceProperty); }
            set { SetValue(ImgSourceProperty, value); }
        }

        public static readonly DependencyProperty ImgSourceProperty =
            DependencyProperty.Register("ImgSource", typeof(ImageSource), ControlType);

        public string Base64Source
        {
            get { return (string)GetValue(Base64SourceProperty); }
            set { SetValue(Base64SourceProperty, value); }
        }

        public static readonly DependencyProperty Base64SourceProperty =
            DependencyProperty.Register("Base64Source", typeof(string), ControlType, new PropertyMetadata(Base64SourceChanged));

        public string ImgText
        {
            get { return (string)GetValue(ImgTextProperty); }
            set { SetValue(ImgTextProperty, value); }
        }

        public static readonly DependencyProperty ImgTextProperty =
            DependencyProperty.Register("ImgText", typeof(string), ControlType);

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

        #region Functions

        private static void Base64SourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as Base64Jpg;
            var base64Content = ctrl.Base64Source;
            ctrl.ImgSource = Base64ToImgSource(base64Content);
        }

        private static ImageSource Base64ToImgSource(string base64Content)
        {
            if (string.IsNullOrEmpty(base64Content))
            {
                return null;
            }

            byte[] contentBytes = Convert.FromBase64String(base64Content);

            var ms = new MemoryStream(contentBytes);
            var imageSourceConverter = new ImageSourceConverter();
            var source = imageSourceConverter.ConvertFrom(ms) as ImageSource;

            return source;
        }

        #endregion Functions
    }
}
