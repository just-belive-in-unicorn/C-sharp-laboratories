using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Exceptions
{
    public class FutureBirthDateException : Exception
    {
        public FutureBirthDateException() : base("Дата народження не може бути в майбутньому.") { }
    }

    public class TooOldBirthDateException : Exception
    {
        public TooOldBirthDateException() : base("Дата народження занадто стара (більше 135 років тому).") { }
    }

    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base("Невірна адреса електронної пошти.") { }
    }

    public class NameContainsDigitsException : Exception
    {
        public NameContainsDigitsException(string fieldName) : base($"{fieldName} не повинно містити цифри.") { }
    }
}
