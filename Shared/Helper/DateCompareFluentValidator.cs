using CandidatePortal.Shared.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidatePortal.Shared.Helper
{
    public class DateCompareFluentValidator : AbstractValidator<CandidateCertificateTypeAddEditDTO>
    {
        public DateCompareFluentValidator()
        {
            //RuleFor(x => x.StartDate)
            //    .NotEmpty()
            //    .Length(1, 10);

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Invalid");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            try
            {
                var result = await ValidateAsync(ValidationContext<CandidateCertificateTypeAddEditDTO>.CreateWithOptions((CandidateCertificateTypeAddEditDTO)model, x => x.IncludeProperties(propertyName)));
                if (result.IsValid)
                    return Array.Empty<string>();
                return result.Errors.Select(e => e.ErrorMessage);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        };

    }
}
