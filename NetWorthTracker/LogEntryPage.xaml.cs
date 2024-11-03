using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace NetWorthTracker
{
    public partial class LogEntryPage : Page
    {
        public LogEntryPage()
        {
            InitializeComponent();
            PopulateAccountFields(UserConfigManager.GetUserAccountNames());
        }

        private void PopulateAccountFields(string[] accountNames)
        {
            int count = 0;

            foreach (string accountName in accountNames)
            {
                Label label = new Label()
                {
                    Content = accountName,
                    Height = 30,
                    HorizontalAlignment = HorizontalAlignment.Right,
                };

                // remove spaces from account name for naming TextBox control
                string _accountName = accountName.Replace(" ", "");

                TextBox textBox = new TextBox()
                {
                    Name = _accountName + "ValueField",
                    Height = 30,
                };

                if (count % 2 == 0)
                {
                    label.Background = new SolidColorBrush(Colors.LightGray);
                    textBox.Background = new SolidColorBrush(Colors.LightGray);
                }

                AccountNamesStackPanel.Children.Add(label);
                AccountValuesStackPanel.Children.Add(textBox);

                count++;
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            string message = "Are you sure you want to return to Main Menu? All unsaved progress will be lost.";
            bool userConfirmed = MessageBox.Show(message, "Confirm", MessageBoxButton.YesNo, 
                                                MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes;

            if (userConfirmed) { NavigationService.GoBack(); }
        }
    }
}
