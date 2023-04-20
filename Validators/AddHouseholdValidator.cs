using FluentValidation;
using HSNP.Models;

namespace HSNP.Mobile.Validators
{
    public class AddHouseholdValidator : AbstractValidator<Household>
    {
        public AddHouseholdValidator()
        {
            RuleFor(x => x.HouseholdMembers).NotNull().WithMessage("1.10 is required\n");
            RuleFor(x => x.HouseholdMembers).GreaterThan(0).WithMessage("1.10 must be greater than 0\n");
        }
    }
    public class AddHouseholdMemberValidator : AbstractValidator<HouseholdMember>
    {
        public AddHouseholdMemberValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("FirstName is required\n");
            RuleFor(x => x.MiddleName).NotNull().WithMessage("MiddleName is required\n");
            RuleFor(x => x.Surname).NotNull().WithMessage("Surname is required\n");
        }
    }
}

