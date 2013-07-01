using FluentValidation;
using RVMS.Win.ViewModels;

namespace RVMS.Win.Models.Validators
{
    public class LinijaViewModelValidator : AbstractValidator<LinijaViewModel>
    {
        public LinijaViewModelValidator()
        {
            RuleFor(x => x.NazivLinije).NotEmpty().WithMessage("Naziv linije nije unet");
        }
    }
}