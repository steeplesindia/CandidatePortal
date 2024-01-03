
using CandidatePortal.Shared.DTO;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CandidatePortal.Shared.Helper
{
    //public class DateCompareValidation : ValidationAttribute
    //{
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        PropertyInfo startDateProperty = validationContext.ObjectType.GetProperty("StartDate");
    //        PropertyInfo endDateProperty = validationContext.ObjectType.GetProperty("EndDate");

    //        if (startDateProperty.GetValue() > endDateProperty.GetValue)
    //        {
    //            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
    //        }
    //        return ValidationResult.Success;
    //    }
    //}

    public class StartDateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (CandidateCertificateTypeAddEditDTO)validationContext.ObjectInstance;
            if (model.EndDate == null)  // Abort if no End Date
            {
                return new ValidationResult("");
                //return ValidationResult.Success;
            }
            DateTime EndDate = model.EndDate.GetValueOrDefault();
            DateTime StartDate = Convert.ToDateTime(value);  // value = StartDate

            if (StartDate > EndDate)
            {
                return new ValidationResult("The start date must be before the end date");
                //return new ValidationResult("The start date must be before the end date");
            }
            else
            { 
                return new ValidationResult("");
            }
            //return ValidationResult.Success;
        }
    }

    public class EndDateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (CandidateCertificateTypeAddEditDTO)validationContext.ObjectInstance;
            if (model.EndDate == null)  // Abort if no End Date
                return ValidationResult.Success;

            DateTime EndDate = Convert.ToDateTime(value); // value = EndDate
            DateTime? StartDate = model.StartDate;

            if (StartDate > EndDate)
                return new ValidationResult("The start date must be before the end date");
            else
                return ValidationResult.Success;
        }
    }

    //public class DateCompareValidation : AbstractValidator
    //{
    //    public DateCompareValidation()
    //    {
    //        //RuleFor(m => m.StartDate)
    //        //    .NotEmpty()
    //        //    .WithMessage("Start Date is Required");

    //        RuleFor(m => m.EndDate)
    //            .NotEmpty()//.WithMessage("End date is required")
    //            .GreaterThan(m => m.StartDate.Value)
    //                            .WithMessage("End date must after Start date")
    //            .When(m => m.StartDate.HasValue);
    //    }
    //}

    //public class BeforeEndDateAttribute : ValidationAttribute
    //{
    //    public string EndDatePropertyName { get; set; }
    //    public string StartDate { get; set; }


    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        PropertyInfo endDateProperty = validationContext.ObjectType.GetProperty(EndDatePropertyName);

    //        DateTime endDate = (DateTime)endDateProperty.GetValue(validationContext.ObjectInstance, null);

    //        var startDate = DateTime.Parse(StartDate);

    //        // Do comparison
    //        // return ValidationResult.Success; // if success
    //        return new ValidationResult("Error"); // if fail
    //    }

    //}

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class LessThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public LessThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            if (value.GetType() == typeof(IComparable))
            {
                throw new ArgumentException("value has not implemented IComparable interface");
            }

            var currentValue = (IComparable)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                throw new ArgumentException("Comparison property with this name not found");
            }

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);

            if (comparisonValue.GetType() == typeof(IComparable))
            {
                throw new ArgumentException("Comparison property has not implemented IComparable interface");
            }

            if (!ReferenceEquals(value.GetType(), comparisonValue.GetType()))
            {
                throw new ArgumentException("The properties types must be the same");
            }

            if (currentValue.CompareTo((IComparable)comparisonValue) >= 0)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class GreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public GreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            if (value.GetType() == typeof(IComparable))
            {
                throw new ArgumentException("value has not implemented IComparable interface");
            }

            var currentValue = (IComparable)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                throw new ArgumentException("Comparison property with this name not found");
            }

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);

            if (comparisonValue.GetType() == typeof(IComparable))
            {
                throw new ArgumentException("Comparison property has not implemented IComparable interface");
            }

            if (!ReferenceEquals(value.GetType(), comparisonValue.GetType()))
            {
                throw new ArgumentException("The properties types must be the same");
            }

            if (currentValue.CompareTo((IComparable)comparisonValue) < 0)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }


    public enum ComparisonType
    {
        LessThan,
        LessThanOrEqualTo,
        EqualTo,
        GreaterThan,
        GreaterThanOrEqualTo
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)] //| AttributeTargets.Field | AttributeTargets.Parameter,
    public class ComparisonAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        private readonly ComparisonType _comparisonType;
        private readonly string _message;

        public ComparisonAttribute(string comparisonProperty, ComparisonType comparisonType, string message)
        {
            _comparisonProperty = comparisonProperty;
            _comparisonType = comparisonType;
            _message = message;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            if (value.GetType() == typeof(IComparable))
            {
                throw new ArgumentException("value has not implemented IComparable interface");
            }

            var currentValue = (IComparable)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                throw new ArgumentException("Comparison property with this name not found");
            }

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);

            if (comparisonValue.GetType() == typeof(IComparable))
            {
                throw new ArgumentException("Comparison property has not implemented IComparable interface");
            }

            if (!ReferenceEquals(value.GetType(), comparisonValue.GetType()))
            {
                throw new ArgumentException("The properties types must be the same");
            }

            bool compareToResult;

            switch (_comparisonType)
            {
                case ComparisonType.LessThan:
                    compareToResult = currentValue.CompareTo((IComparable)comparisonValue) >= 0;
                    break;
                case ComparisonType.LessThanOrEqualTo:
                    compareToResult = currentValue.CompareTo((IComparable)comparisonValue) > 0;
                    break;
                case ComparisonType.EqualTo:
                    compareToResult = currentValue.CompareTo((IComparable)comparisonValue) != 0;
                    break;
                case ComparisonType.GreaterThan:
                    compareToResult = currentValue.CompareTo((IComparable)comparisonValue) <= 0;
                    break;
                case ComparisonType.GreaterThanOrEqualTo:
                    compareToResult = currentValue.CompareTo((IComparable)comparisonValue) < 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return compareToResult ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
        }
    }
}
