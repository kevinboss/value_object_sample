using System;
using System.Text;

namespace value_object_sample.Entities
{
    public class ValidationResult
    {
        private ValidationResult()
        {
        }

        public static ValidationResult CreateValid()
        {
            return new ValidationResult
            {
                IsValid = true
            };
        }

        public static ValidationResult CreateInvalid(params string[] errors)
        {
            return new ValidationResult
            {
                IsValid = false,
                Errors = errors
            };
        }

        public bool IsValid { get; private set; }

        public string[] Errors { get; private set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var error in Errors)
            {
                sb.AppendLine(error);
            }

            return sb.ToString();
        }
    }
}