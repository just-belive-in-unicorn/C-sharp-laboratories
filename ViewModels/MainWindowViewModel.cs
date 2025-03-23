using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private DateTime? _birthDate;
        private string _ageMessage;
        private string _zodiacWestern;
        private string _zodiacChinese;

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public string AgeMessage
        {
            get => _ageMessage;
            private set
            {
                _ageMessage = value;
                OnPropertyChanged(nameof(AgeMessage));
            }
        }

        public string ZodiacWestern
        {
            get => _zodiacWestern;
            private set
            {
                _zodiacWestern = value;
                OnPropertyChanged(nameof(ZodiacWestern));
            }
        }

        public string ZodiacChinese
        {
            get => _zodiacChinese;
            private set
            {
                _zodiacChinese = value;
                OnPropertyChanged(nameof(ZodiacChinese));
            }
        }

        public ICommand CalculateAgeCommand => new RelayCommand(CalculateAge);

        private void CalculateAge()
        {
            if (BirthDate == null)
            {
                MessageBox.Show("Please select a birth date!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime today = DateTime.Today;
            int age = today.Year - BirthDate.Value.Year;
            if (BirthDate.Value.Date > today.AddYears(-age)) age--;

            if (age < 0 || age > 135)
            {
                MessageBox.Show("Invalid age! Please enter a valid birth date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AgeMessage = $"You are {age} years old.";
            ZodiacWestern = $"Western zodiac: {ZodiacHelper.GetWesternZodiac(BirthDate.Value)}";
            ZodiacChinese = $"Chinese zodiac: {ZodiacHelper.GetChineseZodiac(BirthDate.Value)}";

            if (BirthDate.Value.Day == today.Day && BirthDate.Value.Month == today.Month)
            {
                MessageBox.Show("Happy Birthday! 🎉", "Congratulations!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
