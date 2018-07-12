using System;

namespace value_object_sample.Entities.ValueObjects
{
    public class Name
    {
        private Name(string name)
        {
            Value = name;
        }

        public static Name Create(string name)
        {
            var validationResult = IsValid(name);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.ToString(), nameof(name));
            }

            return new Name(name);
        }

        public string Value { get; }

        public static ValidationResult IsValid(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return ValidationResult.CreateInvalid("Can not be empty");
            }

            return ValidationResult.CreateValid();
        }

        protected bool Equals(Name other)
        {
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Name) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}