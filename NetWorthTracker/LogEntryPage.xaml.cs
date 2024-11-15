﻿using System.Windows;
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
            DatePickerControl.Text = DateTime.Today.ToString(Database.DateFormatString);
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
                    HorizontalAlignment = HorizontalAlignment.Left,
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
            if (!AllTextBoxesEmpty())
            {
                string message = "Are you sure you want to return to Main Menu? All unsaved progress will be lost.";
                bool userConfirmed = MessageBox.Show(message, "Confirm", MessageBoxButton.YesNo,
                                                    MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes;
                if (userConfirmed) 
                { 
                    NavigationService.GoBack();
                }
            }
            else 
            { 
                NavigationService.GoBack(); 
            }
        }

        private void SaveLogEntry_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Parse(DatePickerControl.Text);
            NetWorthLogEntry logEntry = new NetWorthLogEntry(date);

            for (int i = 0; i < AccountNamesStackPanel.Children.Count; i++)
            {
                var nameControl = AccountNamesStackPanel.Children[i];
                var valueControl = AccountValuesStackPanel.Children[i];

                if (nameControl is Label nameLabel && valueControl is TextBox valueTextBox)
                {
                    string accountName = nameLabel.Content as string ?? "";
                    accountName = accountName.Replace(" ", "");

                    if (double.TryParse(valueTextBox.Text, out double accountBalance))
                    {
                        logEntry.AddAccountBalance(accountName, accountBalance);
                    }
                }
            }

            bool addRecordSuccessful = Database.InsertRecord(logEntry);
            
            if (addRecordSuccessful)
            {
                MessageBox.Show("The log entry was successfully added to the database.", 
                                "Database Entry Succeeded", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearTextBoxes();
            }
            else
            {
                MessageBox.Show("Failed to write log entry to the database.",
                                "Database Entry Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearTextBoxes()
        {
            foreach (var child in AccountValuesStackPanel.Children)
            {
                if (child is TextBox valueTextBox)
                {
                    valueTextBox.Clear();
                }
            }
        }

        private bool AllTextBoxesEmpty()
        {
            return AccountValuesStackPanel.Children.OfType<TextBox>()
           .All(textBox => string.IsNullOrEmpty(textBox.Text));
        }
    }
}
