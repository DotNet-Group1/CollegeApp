using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Validators
{
    public class DateCheckAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var date = (DateTime)value;
                if (date < DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage ?? "Date should be greater than current date");
                }
            }
            return ValidationResult.Success;
        }
    }
}
