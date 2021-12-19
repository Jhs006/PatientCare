using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PatientCareBL.ValidationAttributes
{
    public class RequiredIfFieldCheckedAttribute:  ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public RequiredIfFieldCheckedAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((bool)value == false)
            {
                return ValidationResult.Success;
            }

            ErrorMessage = ErrorMessageString;
           
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var requiredField = property.GetValue(validationContext.ObjectInstance);

            //var currentValue = (DateTime)value;
            if (requiredField == null)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }


    }

}
