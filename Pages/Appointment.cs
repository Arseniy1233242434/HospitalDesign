using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfApp3.Pages
{
    public class Appointment: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        private string _date;
        public string Date
        {
            get => _date;
            set
            {
                if (value != _date)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _doctor_id;
        public string Doctor_id
        {
            get => _doctor_id;
            set
            {
                if (value != _doctor_id)
                {
                    _doctor_id = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _diagnosis;
        public string Diagnosis
        {
            get => _diagnosis;
            set
            {
                if (value != _diagnosis)
                {
                    _diagnosis = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _recomendations;
        public string Recomendations
        {
            get => _recomendations;
            set
            {
                if (value != _recomendations)
                {
                    _recomendations = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _doctorInfo;
        [JsonIgnore]
        public string DoctorInfo
        {
            get => _doctorInfo;
            set
            {
                if (value != _doctorInfo)
                {
                    _doctorInfo = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
