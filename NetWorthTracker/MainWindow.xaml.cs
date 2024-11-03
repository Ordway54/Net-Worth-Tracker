using System.Windows;

namespace NetWorthTracker
{
    public partial class MainWindow : Window
    { 
    
        readonly DateTime Today = DateTime.Today;
        public MainWindow()
        {
            InitializeComponent();
            DateField.Text = Today.ToString("dddd, MMMM d, yyyy");
        }
    }
}