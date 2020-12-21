#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using DMT.Controls;

#endregion

namespace DMT.Windows
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SignInWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Unloaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SmartcardManager.Instance.UserChanged += Instance_UserChanged;
            //txtUserId.SelectAll();
            //txtUserId.Focus();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            SmartcardManager.Instance.UserChanged -= Instance_UserChanged;
            SmartcardManager.Instance.Shutdown();
        }

        #endregion

        #region Smartcard Handler(s)

        private void Instance_UserChanged(object sender, EventArgs e)
        {
            /*
            if (null != SmartcardManager.Instance.User)
            {
                _user = SmartcardManager.Instance.User;
                if (tabs.SelectedIndex == 0)
                {
                    CheckUser();
                }
                else if (tabs.SelectedIndex == 1)
                {
                    txtUserId2.Text = _user.UserId;
                    txtPassword2.SelectAll();
                    txtPassword2.Focus();
                }
            }
            */
        }

        #endregion
    }
}
