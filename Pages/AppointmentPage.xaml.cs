using System;
using System.Collections.Generic;
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
using System.IO;
using System.Globalization;
using System.Collections.ObjectModel;

namespace WpfApp3.Pages
{
    /// <summary>
    /// Логика взаимодействия для AppointmentPage.xaml
    /// </summary>
    public partial class AppointmentPage : Page
    {
       public Patient Patient { get; set; }
       public Appointment Appointment  { get; set; } = new();
        string id;
        Doctor Doctor1 { get; set; }
        
        public AppointmentPage(Patient a,string id)
        {
            
            Patient = a;
           this.id = id;
            InitializeComponent();
            DataContext = this;
            if(Patient.Appointments==null)
            {
Patient.Appointments = new();
            }
            h();
        }
        public void h()
        {
            foreach(Appointment a in Patient.Appointments)
            {
                string fileName = $"Doctor\\D_{a.Doctor_id}.json";
                var json = File.ReadAllText(fileName);
                Doctor1 = JsonSerializer.Deserialize<Doctor>(json);
                a.DoctorInfo = Doctor1.Name + " " + Doctor1.LastName + " " + Doctor1.MiddleName;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Appointment.Date == "" || Appointment.Diagnosis == "" || Appointment.Recomendations == "" )
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string fileName = $"Doctor\\D_{id}.json";
            var json = File.ReadAllText(fileName);
            Doctor1 = JsonSerializer.Deserialize<Doctor>(json);


            Appointment.Doctor_id = id;
            Appointment.DoctorInfo=Doctor1.Name+" "+Doctor1.LastName+" "+Doctor1.MiddleName;
            Patient.Appointments.Add(Appointment);
            var options1 = new JsonSerializerOptions { WriteIndented = true };
            string json1 = JsonSerializer.Serialize(Patient, options1);
            string fileName1 = $"Patient\\P_{Patient.Id}.json";
            File.WriteAllText(fileName1, json1);
           
            return;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
    public class Converterr : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            var date = DateTime.Now.Year-DateTime.Parse(value.ToString()).Year;
            if(DateTime.Parse(value.ToString())>DateTime.Now.AddYears(-date))
            {
                date--;
            }
           return date;
        }
        public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            return value.ToString();
        }
        
    }
    public class Converter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            var date = DateTime.Now.Year - DateTime.Parse(value.ToString()).Year;
            if (DateTime.Parse(value.ToString()) > DateTime.Now.AddYears(-date))
            {
                date--;
            }
            if(date>=18)
            {
                return "совершеннолетний";
            }
            return "несовершеннолетний";
        }
        public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            return value.ToString();
        }
    }
    public class Converter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            var t = (Patient)value;
            if (t.Appointments.Count==0)
            {
                return "Первый приём";
            }
            
            TimeSpan d = DateTime.Now - DateTime.Parse(t.Appointments[t.Appointments.Count-1].Date);
            return "Дней с предыдущего приёма: "+ (int)d.TotalDays;

        }
        public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            return value.ToString();
        }
    }
    public class IsDateCorrect : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo
        cultureInfo)
        {
            
            //var input = value.ToString().Trim();

            //if (!DateTime.TryParse(input,out DateTime a))
            //{
            //    MessageBox.Show("Неправильный формат даты");
            //    return new ValidationResult(false, "Неправильный формат даты");
            //}
            
            return ValidationResult.ValidResult;
        }
    }
}
//|| DateTime.Now<DateTime.Parse(input)