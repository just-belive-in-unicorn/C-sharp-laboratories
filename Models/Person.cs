using System;

namespace Lab2.Models
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime? BirthDate { get; }

        public bool IsAdult { get; }
        public string SunSign { get; }
        public string ChineseSign { get; }
        public bool IsBirthday { get; }

        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;

            IsAdult = CalculateIsAdult();
            SunSign = CalculateSunSign();
            ChineseSign = CalculateChineseSign();
            IsBirthday = CalculateIsBirthday();
        }

        public Person(string firstName, string lastName, string email)
            : this(firstName, lastName, email, DateTime.MinValue) { }

        public Person(string firstName, string lastName, DateTime birthDate)
            : this(firstName, lastName, "unknown@example.com", birthDate) { }

        public Person(string firstName, string lastName)
            : this(firstName, lastName, "unknown@example.com", DateTime.MinValue) { }

        private bool CalculateIsAdult() => BirthDate.HasValue && (DateTime.Now.Year - BirthDate.Value.Year) >= 18;

        private string CalculateSunSign()
        {
            if (!BirthDate.HasValue) return "Unknown";
            int month = BirthDate.Value.Month;
            int day = BirthDate.Value.Day;

            return month switch
            {
                1 => (day <= 19) ? "Capricorn" : "Aquarius",
                2 => (day <= 18) ? "Aquarius" : "Pisces",
                3 => (day <= 20) ? "Pisces" : "Aries",
                4 => (day <= 19) ? "Aries" : "Taurus",
                5 => (day <= 20) ? "Taurus" : "Gemini",
                6 => (day <= 20) ? "Gemini" : "Cancer",
                7 => (day <= 22) ? "Cancer" : "Leo",
                8 => (day <= 22) ? "Leo" : "Virgo",
                9 => (day <= 22) ? "Virgo" : "Libra",
                10 => (day <= 22) ? "Libra" : "Scorpio",
                11 => (day <= 21) ? "Scorpio" : "Sagittarius",
                12 => (day <= 21) ? "Sagittarius" : "Capricorn",
                _ => "Unknown"
            };
        }

        private string CalculateChineseSign()
        {
            if (!BirthDate.HasValue) return "Unknown";
            string[] signs = { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat" };
            return signs[BirthDate.Value.Year % 12];
        }

        private bool CalculateIsBirthday() =>
            BirthDate.HasValue && BirthDate.Value.Day == DateTime.Now.Day && BirthDate.Value.Month == DateTime.Now.Month;
    }
}
