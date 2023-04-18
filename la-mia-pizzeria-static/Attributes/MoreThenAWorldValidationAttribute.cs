using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Attributes
{
    public class MoreThenAWorldAttribute : ValidationAttribute
    {
        int WordCount;

        public MoreThenAWorldAttribute(int WordCount)
        {
            this.WordCount = WordCount;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? fieldValue = value as string;
            if (fieldValue == null || fieldValue.Trim().Split(' ').Length < WordCount)
            {
                return new ValidationResult($"Il campo deve contenere almeno {WordCount} parole");
            }
            return ValidationResult.Success;
        }
    }
}
