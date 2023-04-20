using FluentValidation;
using HSNP.Models;

namespace HSNP.Mobile.Validators
{
    public class HouseholdValidator : AbstractValidator<Household>
    {
        public HouseholdValidator()
        {
            RuleFor(x => x.StillBirths).NotNull().WithMessage("(2.26) is required\n");
            RuleFor(x => x.StillBirths).NotNull().WithMessage("(2.27) is required\n");
            RuleFor(x => x.HouseholdCutMealId).NotNull().WithMessage("(2.29) is required\n");
           // RuleFor(x => x.FloorMaterialId).NotNull().WithMessage("(2.05) is required\n");
          
        }
    }
}

