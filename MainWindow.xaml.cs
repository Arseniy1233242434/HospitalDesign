using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp3.Pages;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (!Directory.Exists("Doctor"))
                Directory.CreateDirectory("Doctor");

            if (!Directory.Exists("Patient"))
                Directory.CreateDirectory("Patient");
            MainFrame.Navigate(new LogIn());
        }
        private void ChangeThemeButton_Click(object sender, RoutedEventArgs e)
        {
            ThemeHelper.Toggle();
        }
    }
}