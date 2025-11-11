using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
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
using static WpfApp3.Pages.LogIn;

namespace WpfApp3.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        
        public static ObservableCollection<Patient> Patientss { get; set; } = new();
        public Doctor Doctor1 { get; set; }
        public  Patient SelectedUser { get; set; }
        public static  AuthorisationDoctor patientcount { get; set; } = new();


        public MainPage(Doctor Doctor1,string id)
        {
            patientcount.ID = Directory.GetFiles("Patient").Length.ToString();

            patientcount.Password = Directory.GetFiles("Doctor").Length.ToString();
            InitializeComponent();
            loadPatient();
          //  DataContext = Doctor1;
            this.Doctor1 = Doctor1;
            this.Doctor1.Id = id;
            DataContext = this;

        }

        private void Create_Patient(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreatePatient());
            
        }
        public void loadPatient()
        {
            int count = Directory.GetFiles("Patient").Length;
            for (int i = 0; i < count; i++)
            {

                string fileName = $"Patient\\P_{i.ToString("D7")}.json";
                var json = File.ReadAllText(fileName);
                var p = JsonSerializer.Deserialize<Patient>(json);
                p.Id = i.ToString("D7");
                Patientss.Add(p);

            }
        }

        private void SavePatient(object sender, RoutedEventArgs e)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Пациент не выбран!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NavigationService.Navigate(new CreatePatient(SelectedUser));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Пациент не выбран!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NavigationService.Navigate(new AppointmentPage(SelectedUser,Doctor1.Id));
        }

        private void DeletePatient_click(object sender, RoutedEventArgs e)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Пациент не выбран!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string fileName = $"Patient\\P_{SelectedUser.Id.ToString()}.json";
            
            File.Delete(fileName);
            Patientss.Remove(SelectedUser);
        }
    }
    public class DiscountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
           return long.Parse(value.ToString());
        }
        public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
