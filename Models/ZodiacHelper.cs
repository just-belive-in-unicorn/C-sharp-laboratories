using System;

namespace WpfApp1.Models
{
    public static class ZodiacHelper
    {
        public static string GetWesternZodiac(DateTime birthDate)
        {
            int day = birthDate.Day, month = birthDate.Month;
            return month switch
            {
                1 => day <= 19 ? "Capricorn" : "Aquarius",
                2 => day <= 18 ? "Aquarius" : "Pisces",
                3 => day <= 20 ? "Pisces" : "Aries",
                _ => "Other sign"
            };
        }

        public static string GetChineseZodiac(DateTime birthDate)
        {
            string[] animals = { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat" };
            return animals[birthDate.Year % 12];
        }
    }
}
