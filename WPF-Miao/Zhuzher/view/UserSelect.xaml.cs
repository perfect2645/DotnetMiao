using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Zhuzher.search;

namespace Zhuzher.view
{
    /// <summary>
    /// UserSelect.xaml 的交互逻辑
    /// </summary>
    public partial class UserSelect : UserControl
    {
        #region Properties
        private static readonly Type controlType = typeof(UserSelect);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), controlType);

        public List<UserProject> UserList
        {
            get { return (List<UserProject>)GetValue(UserListProperty); }
            set { SetValue(UserListProperty, value); }
        }

        public static readonly DependencyProperty UserListProperty =
            DependencyProperty.Register("UserList", typeof(List<UserProject>), controlType, new PropertyMetadata(SlectedUserChanged));

        public UserProject SelectedUser
        {
            get { return (UserProject)GetValue(SelectedUserProperty); }
            set { SetValue(SelectedUserProperty, value); }
        }

        public static readonly DependencyProperty SelectedUserProperty =
            DependencyProperty.Register("SelectedUser", typeof(UserProject), controlType);

        #endregion Properties

        public UserSelect()
        {
            InitializeComponent();
        }

        private static void SlectedUserChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (UserSelect)d;
            if (target != null)
            {
                return;
            }
            
        }
    }
}
