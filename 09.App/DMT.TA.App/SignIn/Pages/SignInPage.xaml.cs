#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib.Services;

using DMT.Models;
using DMT.Services;
using DMT.Controls;

#endregion

namespace DMT.Pages
{
    /// <summary>
    /// Interaction logic for SignInPage.xaml
    /// </summary>
    public partial class SignInPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SignInPage()
        {
            InitializeComponent();

            txtUserId.SelectAll();
            txtUserId.Focus();
        }

        #endregion

        #region Internal Variables

        private List<string> _roles = new List<string>();
        private User _user = null;

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SmartcardManager.Instance.UserChanged += Instance_UserChanged;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            SmartcardManager.Instance.UserChanged -= Instance_UserChanged;
            SmartcardManager.Instance.Shutdown();
        }

        #endregion

        #region Smartcard Handler(s)

        private void Instance_UserChanged(object sender, EventArgs e)
        {
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
        }

        #endregion

        #region Button Handler(s)

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (tabs.SelectedIndex != 0) return;
            txtMsg.Text = string.Empty;

            string userId = txtUserId.Text.Trim();
            string pwd = txtPassword.Password.Trim();
            if (string.IsNullOrWhiteSpace(userId))
            {
                txtUserId.SelectAll();
                txtUserId.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(pwd))
            {
                txtPassword.SelectAll();
                txtPassword.Focus();
                return;
            }

            var md5 = Utils.MD5.Encrypt(pwd);
            _user = User.GetByLogIn(userId, md5).Value();

            CheckUser();
        }

        private void cmdChangePwd_Click(object sender, RoutedEventArgs e)
        {
            txtPassword.Password = string.Empty;

            txtUserId2.Text = string.Empty;
            txtPassword2.Password = string.Empty;
            txtNewPassword.Password = string.Empty;
            txtConfirmPassword.Password = string.Empty;

            tabs.SelectedIndex = 1;
        }

        private void cmdOK2_Click(object sender, RoutedEventArgs e)
        {
            // Call change password.
            if (ChangePassword())
            {
                txtPassword.Password = string.Empty;

                txtUserId2.Text = string.Empty;
                txtPassword2.Password = string.Empty;
                txtNewPassword.Password = string.Empty;
                txtConfirmPassword.Password = string.Empty;

                tabs.SelectedIndex = 0;
            }
        }

        private void cmdCancel2_Click(object sender, RoutedEventArgs e)
        {
            txtPassword.Password = string.Empty;

            txtUserId2.Text = string.Empty;
            txtPassword2.Password = string.Empty;
            txtNewPassword.Password = string.Empty;
            txtConfirmPassword.Password = string.Empty;

            tabs.SelectedIndex = 0;
        }

        #endregion

        #region TextBox Keydown

        private void txtUserId_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tabs.SelectedIndex != 0) return;
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtPassword.SelectAll();
                txtPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tabs.SelectedIndex != 0) return;
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                cmdOK.Focus();
                e.Handled = true;
            }
        }

        private void txtUserId2_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tabs.SelectedIndex != 1) return;
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtPassword2.SelectAll();
                txtPassword2.Focus();
                e.Handled = true;
            }
        }

        private void txtPassword2_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tabs.SelectedIndex != 1) return;
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtNewPassword.SelectAll();
                txtNewPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtNewPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tabs.SelectedIndex != 1) return;
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtConfirmPassword.SelectAll();
                txtConfirmPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtConfirmPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tabs.SelectedIndex != 1) return;
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                cmdOK2.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region Private Methods

        private void CheckUser()
        {
            if (null == _user || _roles.IndexOf(_user.RoleId) == -1)
            {
                txtMsg.Text = "LogIn Failed";
                txtUserId.SelectAll();
                txtUserId.Focus();
                return;
            }

            SmartcardManager.Instance.Shutdown();
            Controls.TAApp.User.Current = _user;
            // Init Main Menu
            PageContentManager.Instance.Current = new TA.Pages.Menu.MainMenu();
        }

        private bool ChangePassword()
        {
            bool ret = false;

            var userId = txtUserId2.Text.Trim();
            var md5 = Utils.MD5.Encrypt(txtPassword2.Password);
            _user = User.GetByLogIn(userId, md5).Value();
            if (null == _user)
            {
                txtMsg2.Text = "Staff Not Found.";
                txtUserId2.SelectAll();
                txtUserId2.Focus();
                return ret;
            }
            var oldPwd = Utils.MD5.Encrypt(txtPassword2.Password);
            if (_user.Password != oldPwd)
            {
                txtMsg2.Text = "Old Password not match.";
                txtPassword2.SelectAll();
                txtPassword2.Focus();
                return ret;
            }

            var newPwd = Utils.MD5.Encrypt(txtNewPassword.Password);
            var confPwd = Utils.MD5.Encrypt(txtConfirmPassword.Password);
            if (newPwd != confPwd)
            {
                txtMsg2.Text = "Confirm Password mismatch.";
                txtConfirmPassword.SelectAll();
                txtConfirmPassword.Focus();
                return ret;
            }

            _user.Password = newPwd; // change password.
            var saveRet = User.SaveUser(_user);
            if (!saveRet.Ok)
            {
                txtMsg2.Text = "Save failed.";
                txtUserId2.SelectAll();
                txtUserId2.Focus();
                return ret;
            }
            ret = true;
            //TODO: Need to implements SCW message to send later.
            /*
            // send to server
            SCWOperations server = SCWServiceOperations.Instance.Plaza;
            var inst = new SCWChangePassword();
            inst.staffId = _user.UserId;
            inst.password = oldPwd;
            inst.newPassword = newPwd;
            inst.confirmNewPassword = confPwd;
            // send.
            SCWServiceOperations.Instance.UserName = "DMTUSER";
            SCWServiceOperations.Instance.Password = "DMTPASS";
            server.TOD.ChangePassword(inst);
            */
            return ret;
        }

        #endregion

        #region Public Methods

        public void Setup(params string[] roles)
        {
            _roles.Clear();
            _roles.AddRange(roles);

            SmartcardManager.Instance.Start();
        }

        public User User { get { return _user; } }

        #endregion
    }
}
