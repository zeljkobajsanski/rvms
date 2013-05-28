using System;
using FluentValidation;
using FluentValidation.Internal;
using Korisnici.ClientLibrary;
using RVMS.Win.Models.Validators;
using System.Linq;

namespace RVMS.Win.ViewModels
{
    public class LoginViewModel : ViewModel
    {
        private string fKorisnickoIme;
        private string fLozinka;
        private readonly LoginViewModelValidator fValidator = new LoginViewModelValidator();
        private string fMessage;

        public string KorisnickoIme
        {
            get { return fKorisnickoIme; }
            set
            {
                if (value == fKorisnickoIme) return;
                fKorisnickoIme = value;
                OnPropertyChanged("KorisnickoIme");
            }
        }

        public string Lozinka
        {
            get { return fLozinka; }
            set
            {
                if (value == fLozinka) return;
                fLozinka = value;
                OnPropertyChanged("Lozinka");
            }
        }

        public string Message
        {
            get { return fMessage; }
            set
            {
                if (value == fMessage) return;
                fMessage = value;
                OnPropertyChanged("Message");
            }
        }

        public override string this[string columnName]
        {
            get
            {
                var vr = fValidator.Validate(this);
                var msg = vr.Errors.FirstOrDefault(x => x.PropertyName == columnName);
                return msg != null ? msg.ErrorMessage : null;
            }
        }

        public bool PrijaviMe()
        {
            Message = null;
            if (!fValidator.Validate(this).IsValid)
            {
                Message = "Podaci za prijavu nisu ispravni";
                return false;
            }
            var account = new Account(ApplicationContext.Current.LoginService);
            try
            {
                var loginInfo = account.Login(KorisnickoIme, Lozinka, "rvms");
                if (loginInfo.Status == "Ok")
                {
                    ApplicationContext.Current.LogId = loginInfo.LogId;
                    ApplicationContext.Current.IdKorisnika = loginInfo.UserId;
                    ApplicationContext.Current.KorisnickoIme = loginInfo.Username;
                    ApplicationContext.Current.ImeIPrezime = loginInfo.Name;
                    return true;
                }

                Message = loginInfo.Message;
                return false;
            }
            catch (Exception exc)
            {
                Message = exc.Message;
                return false;
            }
        }
    }
}