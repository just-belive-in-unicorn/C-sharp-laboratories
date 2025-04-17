using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Lab2.Commands;
using Lab2.Models;
using Lab2.Exceptions;

namespace Lab2.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string email;
        private string birthDate;
        private bool isProcessing;

        public string FirstName
        {
            get => firstName;
            set { firstName = value; OnPropertyChanged(nameof(FirstName)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public string LastName
        {
            get => lastName;
            set { lastName = value; OnPropertyChanged(nameof(LastName)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged(nameof(Email)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public string BirthDate
        {
            get => birthDate;
            set { birthDate = value; OnPropertyChanged(nameof(BirthDate)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public bool IsProcessing
        {
            get => isProcessing;
            private set { isProcessing = value; OnPropertyChanged(nameof(IsProcessing)); }
        }

        public bool CanProceed =>
            !string.IsNullOrWhiteSpace(FirstName) &&
            !string.IsNullOrWhiteSpace(LastName) &&
            !string.IsNullOrWhiteSpace(Email) &&
            IsValidEmail(Email) &&
            !string.IsNullOrWhiteSpace(BirthDate);

        public ICommand ProceedCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public PersonViewModel()
        {
            ProceedCommand = new RelayCommand(async () => await ProceedAsync(), () => CanProceed && !IsProcessing);
        }

    private async Task ProceedAsync()
    {
        IsProcessing = true;

        try
        {
            ValidateInput();

            DateTime birthDateValue = DateTime.Parse(BirthDate);

            Person person = await Task.Run(() => new Person(FirstName, LastName, Email, birthDateValue));

            if (person.IsBirthday)
            {
                MessageBox.Show("Happy Birthday!", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            MessageBox.Show($"First Name: {person.FirstName}\nLast Name: {person.LastName}\nEmail: {person.Email}\n" +
                            $"Birth Date: {person.BirthDate?.ToShortDateString()}\nAdult: {person.IsAdult}\n" +
                            $"Western Zodiac: {person.SunSign}\nChinese Zodiac: {person.ChineseSign}\nBirthday Today: {person.IsBirthday}",
                            "Results", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsProcessing = false;
        }
    }

    private void ValidateInput()
    {
        if (!DateTime.TryParse(BirthDate, out DateTime birthDateValue))
            throw new FormatException("Невірний формат дати народження.");

        if (birthDateValue > DateTime.Now)
            throw new FutureBirthDateException();

        if (DateTime.Now.Year - birthDateValue.Year > 135)
            throw new TooOldBirthDateException();

        if (!IsValidEmail(Email))
            throw new InvalidEmailException();

        if (Regex.IsMatch(FirstName, @"\d"))
            throw new NameContainsDigitsException("Ім’я");

        if (Regex.IsMatch(LastName, @"\d"))
            throw new NameContainsDigitsException("Прізвище");
    }


    private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
