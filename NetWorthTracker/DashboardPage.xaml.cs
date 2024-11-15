using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace NetWorthTracker
{
    public partial class DashboardPage : Page
    {
        public DashboardPage()
        {
            InitializeComponent();
            Plotting.DisplayNetWorthPlot(WpfPlot1);
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
