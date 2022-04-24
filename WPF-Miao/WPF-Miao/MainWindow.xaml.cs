﻿using Logging;
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
using WPF_Miao.Platform;

namespace WPF_Miao
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DianYiTong_Click(object sender, RoutedEventArgs e)
        {
            new DianYiTongMain();
        }

        private void SubmitOrder_Click(object sender, RoutedEventArgs e)
        {
            GLog.TestLogger();
            var submitOrder = new SubmitOrder();
            submitOrder.TrySubmitOrder();
        }
    }
}
