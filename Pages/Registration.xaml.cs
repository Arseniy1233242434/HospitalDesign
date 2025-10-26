using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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

namespace WpfApp3.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        Doctor Doctor = new Doctor();
        public Registration()
        {
            InitializeComponent();
            DataContext = Doctor;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            if (Doctor.Name == "" || Doctor.LastName == "" || Doctor.MiddleName == "" || Doctor.Name == "" || Doctor.Specialization == "" || Doctor.Password == "" || Doctor.SecondPassword == "")
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (Doctor.Password != Doctor.SecondPassword)
                {
                    MessageBox.Show("Пароли должны совпадать!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    int count = Directory.GetFiles("Doctor").Length;
                    string fileName = $"Doctor\\D_{count.ToString("D5")}.json";
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string json = JsonSerializer.Serialize(Doctor, options);
                    File.WriteAllText(fileName, json);
                    MessageBox.Show($"Регистрация завершена Ваш логин D_{count.ToString("D5")}");
                }
            }
            NavigationService.GoBack();
            //  UpdateCount();
        }
    }
}
