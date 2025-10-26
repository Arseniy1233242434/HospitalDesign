using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
namespace WpfApp3.Pages
{
    /// <summary>
    /// Логика взаимодействия для LogIn.xaml
    /// </summary>
    public partial class LogIn : Page
    {
        Doctor Doctor1 = new Doctor();
        AuthorisationDoctor  a { get; set; } = new ();
        public LogIn()
        {
            InitializeComponent();
            DataContext = a;
        }
        public class AuthorisationDoctor : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string? propName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }
            private string _id;
            public string ID
            {
                get => _id;
                set
                {
                    if (value != _id)
                    {
                        _id = value;
                        OnPropertyChanged();
                    }
                }
            }

            private string _password;
            public string Password
            {
                get => _password;
                set
                {
                    if (value != _id)
                    {
                        _password = value;
                        OnPropertyChanged();
                    }
                }
            }
        }
        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }

        private void Authorisation_Click(object sender, RoutedEventArgs e)
        {
            string fileName = $"Doctor\\D_{a.ID}.json";
            if (a.ID == "" || a.Password == "")
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!File.Exists(fileName))
            {
                MessageBox.Show("Не правильный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var json = File.ReadAllText(fileName);
            if (JsonSerializer.Deserialize<Doctor>(json).Password != a.Password)
            {
                MessageBox.Show("Не правильный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Doctor1 = JsonSerializer.Deserialize<Doctor>(json);
            NavigationService.Navigate(new MainPage(Doctor1, a.ID));
        }
    }
}
