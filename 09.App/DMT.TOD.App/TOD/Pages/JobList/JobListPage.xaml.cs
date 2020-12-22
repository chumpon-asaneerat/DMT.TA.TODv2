#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using NLib.Services;
using NLib.Reflection;

#endregion

namespace DMT.TOD.Pages.Jobs
{
    /// <summary>
    /// Interaction logic for JobListPage.xaml
    /// </summary>
    public partial class JobListPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public JobListPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        #endregion

        private void Refresh()
        {

        }

        public void Setup(User user)
        {

        }
    }
}
