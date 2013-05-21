using FluentValidation;
using RVMS.Win.ViewModels;

namespace RVMS.Win.Models.Validators
{
    public class RelacijeViewModelValidator : AbstractValidator<RelacijaViewModel>
    {
        public RelacijeViewModelValidator()
        {
            RuleFor(x => x.NazivRelacije).NotEmpty().WithMessage("Unesite naziv relacije");
        }
    }
}