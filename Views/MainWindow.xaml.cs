using System.Windows;
using WpfApp1.ViewModels;


namespace BirthdayApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
