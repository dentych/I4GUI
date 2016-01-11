using Desktop.Magic;
using Desktop.ViewModels;
using System.Windows;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModelDecider.Instance;
            ViewModelDecider.Instance.ViewModel = new MainWindowVM();
        }

        private void MenuHighscore_Click(object sender, RoutedEventArgs e)
        {
            IModalDialog dialog = new ModalDialogLocator();
            var vm = new HighscoreVM();
            dialog.ShowDialog("Highscore", vm);
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            ViewModelDecider.Instance.ViewModel = new MainWindowVM();
        }
    }
}
