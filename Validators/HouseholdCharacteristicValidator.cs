using System;
using FluentValidation;
using HSNP.Models;

namespace HSNP.Mobile.Validators
{

    public class HouseholdCharacteristicValidator : AbstractValidator<HouseholdCharacteristic>
    {
        public HouseholdCharacteristicValidator()
        {
            RuleFor(x => x.NoMainRooms).NotNull().WithMessage("(2.01) is required\n");
           // RuleFor(x => x.Tenure).NotNull().WithMessage("(2.02) is required\n");
            RuleFor(x => x.RoofMaterialId).NotNull().WithMessage("(2.03) is required\n");
            RuleFor(x => x.WallMaterialId).NotNull().WithMessage("(2.04) is required\n");
            RuleFor(x => x.FloorMaterialId).NotNull().WithMessage("(2.05) is required\n");
            // RuleFor(x => x.DwellingRiskId).NotNull().WithMessage("(2.06) is required");
            RuleFor(x => x.DrinkWaterId).NotNull().WithMessage("(2.07) is required\n");
            RuleFor(x => x.HHToiletId).NotNull().WithMessage("(2.08) is required\n");
            RuleFor(x => x.CookFuelId).NotNull().WithMessage("(2.09) is required\n");
            RuleFor(x => x.LightfuelId).NotNull().WithMessage("(2.10) is required\n");
            RuleFor(x => x.TVOwnedId).NotNull().WithMessage("(2.11) is required\n");
            RuleFor(x => x.MotorCycleOwnedId).NotNull().WithMessage("(2.12) is required\n");
            RuleFor(x => x.TuktukOwnedId).NotNull().WithMessage("(2.13) is required\n");
            RuleFor(x => x.RefrigeratorOwnedId).NotNull().WithMessage("(2.14) is required\n");
            RuleFor(x => x.CarOwnedId).NotNull().WithMessage("(2.15) is required\n");
            RuleFor(x => x.MobileOwnedId).NotNull().WithMessage("(2.16) is required\n");
            RuleFor(x => x.BicycleOwnedId).NotNull().WithMessage("(2.17) is required\n");

        }
    }
}

