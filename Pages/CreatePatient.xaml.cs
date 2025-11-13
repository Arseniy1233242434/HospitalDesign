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

namespace WpfApp3.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreatePatient.xaml
    /// </summary>
    public partial class CreatePatient : Page
    {
        static public bool phone=false;
        Patient Patient=new Patient();
       
        public CreatePatient()
        {
           
            InitializeComponent();
            DataContext=Patient;
           
        }
        bool f = true;
        public CreatePatient(Patient d)
        {

            InitializeComponent();
            Patient = d;
            DataContext = Patient;
            f= false;

        }
        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            
            if (Patient.Name == "" || Patient.LastName == "" || Patient.MiddleName == "" || Patient.Name == "" || Patient.Birthday == "" || Patient.PhoneNumber == "")
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(!phone)
            {
                MessageBox.Show("Непрвавильный формат номера!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!f)
            {
                var options1 = new JsonSerializerOptions { WriteIndented = true };
                string json1 = JsonSerializer.Serialize(Patient, options1);
                string fileName1 = $"Patient\\P_{Patient.Id}.json";
                File.WriteAllText(fileName1, json1);
                NavigationService.GoBack();
                return;
            }
            int count = Directory.GetFiles("Patient").Length;
            string fileName = $"Patient\\P_{count.ToString("D7")}.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            
            string json = JsonSerializer.Serialize(Patient, options);
            File.WriteAllText(fileName, json);
            MessageBox.Show($"Пациент добавлен P_{count.ToString("D7")}");
            Patient.Id = count.ToString("D7");

            MainPage.patientcount.ID = Directory.GetFiles("Patient").Length.ToString();
            MainPage.patientcount.Password = Directory.GetFiles("Doctor").Length.ToString();
            MainPage.Patientss.Add(Patient);
            NavigationService.GoBack();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
    public class IsNumberCorrect : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo
        cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if ( input.Length != 11)
            {
                CreatePatient.phone = false;
                return new ValidationResult(false, "Неправильный формат номера");
            }
            for(int i=0;i<11;i++)
            {
                if (!char.IsNumber(input[i]))
                {
                    CreatePatient.phone = false;
                    return new ValidationResult(false, "Неправильный формат номера");
                }

            }
            CreatePatient.phone = true;
            return ValidationResult.ValidResult;
        }
    }
    public class IsStringCorrect : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo
        cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            
            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsLetter(input[i]))
                {
                    CreatePatient.phone = false;
                    return new ValidationResult(false, "Неправильный формат");
                }

            }
            CreatePatient.phone = true;
            return ValidationResult.ValidResult;
        }
    }
}
