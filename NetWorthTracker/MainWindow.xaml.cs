using System.Windows;

namespace NetWorthTracker
{
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainMenuPage());
        }
    }
}