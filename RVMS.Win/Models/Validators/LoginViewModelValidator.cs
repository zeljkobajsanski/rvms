using FluentValidation;
using RVMS.Win.ViewModels;

namespace RVMS.Win.Models.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.KorisnickoIme).NotEmpty().WithMessage("Unesite korisničko ime");
            RuleFor(x => x.Lozinka).NotEmpty().WithMessage("Unesite lozinku");
        }
    }
}