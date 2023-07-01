using System.ComponentModel.DataAnnotations;

namespace Domain.CustomValidations
{
    public class EnumValidationAttribute : ValidationAttribute
    {
        private readonly Type _enumType;

        private const string DefaultErrorMessage = "Invalid value. Allowed values are: {0}";
        public EnumValidationAttribute(Type enumType)
        {
            _enumType = enumType;
            ErrorMessage = GetDefaultErrorMessage();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (!IsDefinedIgnoreCase(_enumType, (string) value))
            {
                var enumValues = Enum.GetValues(_enumType);
                var allowedValues = string.Join(", ", enumValues.Cast<object>());

                return new ValidationResult(string.Format(ErrorMessage, allowedValues));
            }

            return ValidationResult.Success;
        }
        private string GetDefaultErrorMessage()
        {
            var enumValues = Enum.GetValues(_enumType);
            var allowedValues = string.Join(", ", enumValues.Cast<object>());
            return string.Format(DefaultErrorMessage, allowedValues);
        }
        private  bool IsDefinedIgnoreCase(Type enumType, string value)
        {
            if (!enumType.IsEnum)
                throw new ArgumentException("The specified type is not an enumeration.");

            foreach (string name in Enum.GetNames(enumType))
            {
                if (name.Equals(value, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
