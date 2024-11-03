﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace NetWorthTracker
{

    public partial class MainMenuPage : Page
    {
        readonly DateTime TodayDate = DateTime.Today;
        const string DateFormatString = "dddd, MMMM d, yyyy";
        public MainMenuPage()
        {
            InitializeComponent();
            DateField.Text = TodayDate.ToString(DateFormatString);
            
        }

        private void AddLogEntry_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LogEntryPage());
        }

        private void ManageAccounts_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccountManagerPage());
        }

        private void ViewDashboard_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DashboardPage());
        }
    }
}
