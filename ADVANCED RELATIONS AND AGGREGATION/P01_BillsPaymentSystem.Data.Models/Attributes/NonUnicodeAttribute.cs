using System.ComponentModel.DataAnnotations;

namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
    public class NonUnicodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var nullError = "Value can NOT be null";
            if (validationContext == null)
            {
                return new ValidationResult(nullError);
            }
            var text = value.ToString();
            var unicodeCharError = "Unicode NOT allowed";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] > 255)
                {
                    return new ValidationResult(unicodeCharError);
                }
            }
            return ValidationResult.Success;
        }
    }
}