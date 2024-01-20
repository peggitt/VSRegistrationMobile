using FluentValidation;
using HSNP.Models;

namespace HSNP.Mobile.Validators
{
    public class MemberValidator : AbstractValidator<HouseholdMember>
    {
        public MemberValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("(3.01) First Name is required\n");
            RuleFor(x => x.MiddleName).NotNull().WithMessage("3.01) Middle Name is required\n");
            RuleFor(x => x.Surname).NotNull().WithMessage("(3.01) Surname is required\n");

         

            RuleFor(x => x.FatherAliveId).NotNull().WithMessage("(3.08)Is the Father Alive is required\n");
            RuleFor(x => x.MotherAliveId).NotNull().WithMessage("(3.09)Is the Mother Alive is required\n");

            RuleFor(x => x.DisabilityId).NotNull().WithMessage("(3.11) Does the Member  have a disability is required\n");

           // RuleFor(x => x.WorkingId).NotNull().WithMessage("\n");


            // RuleFor(x => x.FloorMaterialId).NotNull().WithMessage("(2.05) is required\n");

        }
    }
}

