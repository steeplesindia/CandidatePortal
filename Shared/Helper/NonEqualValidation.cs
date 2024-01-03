using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.Helper
{
    //public class NonEqualValidation
    //{

    //}

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class NonEqualValidation : ValidationAttribute//, IClientValidatable
    {
        //private const string DefaultErrorMessage = "Duplicate e-mail not allowed";
        public string OtherProperty { get; private set; }
        public string OtherPropertyName { get; private set; }

        public NonEqualValidation(string otherProperty, string otherPropertyName) //: base(DefaultErrorMessage)
        {
            OtherProperty = otherProperty;
            OtherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(OtherProperty);

                var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

                if (value.Equals(otherPropertyValue))
                {
                    return new ValidationResult(OtherPropertyName);//FormatErrorMessage(validationContext.DisplayName)
                }
            }

            return ValidationResult.Success;

        }

        //public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        //{
        //    var rule = new ModelClientValidationRule()
        //    {
        //        ValidationType = "unlike",
        //        ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
        //    };

        //    rule.ValidationParameters.Add("otherproperty", OtherProperty);
        //    rule.ValidationParameters.Add("otherpropertyname", OtherPropertyName);

        //    yield return rule;
        //}

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, OtherPropertyName);
        }
    }
}
