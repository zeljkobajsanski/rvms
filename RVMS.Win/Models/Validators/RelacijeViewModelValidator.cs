using FluentValidation;
using RVMS.Win.ViewModels;

namespace RVMS.Win.Models.Validators
{
    public class RelacijeViewModelValidator : AbstractValidator<RelacijaViewModel>
    {
        public RelacijeViewModelValidator()
        {
            RuleFor(x => x.NazivRelacije).NotEmpty().WithMessage("Unesite naziv relacije");
            RuleFor(x => x.Razdaljina).GreaterThanOrEqualTo(0).WithMessage("Unesite razdaljinu");
            RuleFor(x => x.VremeVoznje).GreaterThanOrEqualTo(0).WithMessage("Unesite vreme vožnje");
            //RuleSet("Naziv", () => RuleFor(x => x.NazivRelacije).NotEmpty().WithMessage("Unesite naziv relacije"));
            //RuleSet("RazdaljinaIVreme", () =>
            //{
            //    RuleFor(x => x.Razdaljina).GreaterThan(0).WithMessage("Unesite pozitivnu razdaljinu");
            //    RuleFor(x => x.VremeVoznje).GreaterThan(0).WithMessage("Unesite pozitivno vreme vožnje");
            //});
        }
    }
}