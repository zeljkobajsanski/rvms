using FluentValidation;
using FluentValidation.Results;
using RVMS.Win.ViewModels;

namespace RVMS.Win.Models.Validators
{
    public class PromenaLozinkeViewModelValidator : AbstractValidator<PromenaLozinkeViewModel>
    {
        public PromenaLozinkeViewModelValidator()
        {
            RuleFor(x => x.NovaLozinka).NotNull().WithMessage("Unesite lozinku");
            RuleFor(x => x.PonoviLozinku).NotNull().WithMessage("Unesite potvrdu lozinke");
            Custom(x => x.NovaLozinka != x.PonoviLozinku ? new ValidationFailure("PonoviLozinku", "Potvrda lozinke se ne poklapa") : null);
        }
    }
}