using System;
using System.Collections.Generic;

namespace value_object_sample.Entities.ValueObjects
{
    public class PhoneNumber
    {
        private PhoneNumber(string number)
        {
            Number = number;
        }

        public static PhoneNumber Create(string number)
        {
            var validationResult = IsValid(number);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.ToString(), nameof(number));
            }

            return new PhoneNumber(number);
        }

        public string Number { get; }

        public PhoneNumberPrefix Prefix => PhoneNumberPrefix.Create(Number.Substring(0, 3));

        public static ValidationResult IsValid(string number)
        {
            var erros = new List<string>();
            if (!IsNumeric(number))
            {
                erros.Add("Must be numeric");
            }

            if (number.Length != 10)
            {
                erros.Add("Must be exactly 10 digits long");
            }

            if (erros.Count > 0)
            {
                return ValidationResult.CreateInvalid(erros.ToArray());
            }

            return ValidationResult.CreateValid();
        }

        protected bool Equals(PhoneNumber other)
        {
            return string.Equals(Number, other.Number);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PhoneNumber) obj);
        }

        public override int GetHashCode()
        {
            return (Number != null ? Number.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return Number;
        }

        private static bool IsNumeric(string value)
        {
            return long.TryParse(value, out _);
        }
    }
}