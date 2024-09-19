using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Tennis.Application.Services.Utils
{
    public class ValidateDateTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (DateTime.TryParseExact(value.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime) || DateTime.TryParse(value.ToString(), out parsedDateTime))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid date format. The format should be 'YYYY-MM-DD'.");
        }
    }
}
