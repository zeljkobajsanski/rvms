using System;
using System.Linq;
using Korisnici.ClientLibrary;
using RVMS.Win.Models.Validators;

namespace RVMS.Win.ViewModels
{
    public class PromenaLozinkeViewModel : ViewModel
    {
        private string m_NovaLozinka;
        private string m_PonoviLozinku;
        private PromenaLozinkeViewModelValidator m_Validator = new PromenaLozinkeViewModelValidator();

        public string NovaLozinka
        {
            get { return m_NovaLozinka; }
            set
            {
                if (value == m_NovaLozinka)
                {
                    return;
                }
                m_NovaLozinka = value;
                OnPropertyChanged("NovaLozinka");
            }
        }

        public string PonoviLozinku
        {
            get { return m_PonoviLozinku; }
            set
            {
                if (value == m_PonoviLozinku)
                {
                    return;
                }
                m_PonoviLozinku = value;
                OnPropertyChanged("PonoviLozinku");
            }
        }

        public void Sacuvaj()
        {
            if (!m_Validator.Validate(this).IsValid)
            {
                throw new Exception("Podaci nisu ispravni");
            }
            if (ApplicationContext.Current.IdKorisnika != 0)
            {
                new Account(ApplicationContext.Current.LoginService).ChangePassword(ApplicationContext.Current.IdKorisnika, NovaLozinka);
            }
        }

        public override string this[string columnName]
        {
            get
            {
                var r = m_Validator.Validate(this);
                var fe = r.Errors.FirstOrDefault(x => x.PropertyName == columnName);
                return fe != null ? fe.ErrorMessage : null;
            }
        }
    }
}