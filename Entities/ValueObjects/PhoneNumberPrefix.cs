using System;

namespace value_object_sample.Entities.ValueObjects
{
    public class PhoneNumberPrefix
    {
        private PhoneNumberPrefix(string prefix)
        {
            Prefix = prefix;
        }

        public static PhoneNumberPrefix Create(string prefix)
        {
            var validationResult = IsValid(prefix);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.ToString(), nameof(prefix));
            }
            return new PhoneNumberPrefix(prefix);
        }

        public string Prefix { get; }

        public static ValidationResult IsValid(string prefix)
        {
            if (prefix.Length < 3)
            {
                return ValidationResult.CreateInvalid("Prefix must be at least 3 characters long");
            }
            
            return ValidationResult.CreateValid();
        }

        protected bool Equals(PhoneNumberPrefix other)
        {
            return string.Equals(Prefix, other.Prefix);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PhoneNumberPrefix) obj);
        }

        public override int GetHashCode()
        {
            return (Prefix != null ? Prefix.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return Prefix;
        }
    }
}