#region Using

using System;
using System.Windows;
using System.Windows.Controls;

using NLib.Services;

#endregion

namespace DMT.TA.Pages.Menu
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdRequestExchange_Click(object sender, RoutedEventArgs e)
        {
            // ยืม/แลก เงินยืมทอนฝ่ายบัญชี
        }

        private void cmdReturnExchange_Click(object sender, RoutedEventArgs e)
        {
            // คืนเงินยืมทอนฝ่ายบัญชี
        }

        private void cmdInHouseExchange_Click(object sender, RoutedEventArgs e)
        {
            // แลกเงินหมุนเวียนในด่าน
        }

        private void cmdCouponSoldByPlaza_Click(object sender, RoutedEventArgs e)
        {
            // หัวหน่าขายคูปอง
        }

        private void cmdCouponSoldHistory_Click(object sender, RoutedEventArgs e)
        {
            // ประวัติการขายคูปอง
        }

        private void cmdCreditTransactionSummaryReport_Click(object sender, RoutedEventArgs e)
        {
            // รายงานสรุปการยืมเงินทอน
        }

        private void cmdUserCreditManage_Click(object sender, RoutedEventArgs e)
        {
            // เงินยืมทอน (collector)
        }

        private void cmdUserBorrowCoupon_Click(object sender, RoutedEventArgs e)
        {
            // รับคูปอง (collector)
        }

        private void cmdUserReturnCoupon_Click(object sender, RoutedEventArgs e)
        {
            // คืนคูปอง (collector)
        }

        private void cmdUserCreditHistory_Click(object sender, RoutedEventArgs e)
        {
            // ประวัติการแลกเงินยืมทอน (collector)
        }

        private void cmdCheckBalance_Click(object sender, RoutedEventArgs e)
        {
            // เช็คยอดด่าน
        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            // ออกจากระบบ
            // When enter Sign In Screen reset current user.
            Controls.TAApp.User.Current = null;

            var page = new DMT.Pages.SignInPage();
            page.Setup(
                "ADMINS",
                "ACCOUNT",
                "CTC_MGR", "CTC", /*"TC",*/
                "MT_ADMIN", "MT_TECH",
                "FINANCE", "SV",
                "RAD_MGR", "RAD_SUP");
            PageContentManager.Instance.Current = page;
        }

        private void cmdSetting_Click(object sender, RoutedEventArgs e)
        {
            // ตั้งค่า
        }

        #endregion
    }
}
