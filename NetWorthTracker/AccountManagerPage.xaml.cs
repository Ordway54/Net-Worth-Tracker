using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace NetWorthTracker
{
    public partial class AccountManagerPage : Page
    {
        public AccountManagerPage()
        {
            InitializeComponent();
            PopulateAccountFields();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void PopulateAccountFields()
        {
            string[] accountNames = UserConfigManager.GetUserAccountNames();

            foreach (string accountName in accountNames)
            {
                Label label = new Label()
                {
                    Content = accountName,
                };

                AccountNameStackPanel.Children.Add(label);
            }
        }
    }
}
