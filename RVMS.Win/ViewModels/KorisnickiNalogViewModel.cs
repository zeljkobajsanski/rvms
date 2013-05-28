using Korisnici.ClientLibrary;
using Korisnici.ClientLibrary.WebServices.AccountsService;

namespace RVMS.Win.ViewModels
{
    public class KorisnickiNalogViewModel : ViewModel
    {
        private readonly Account m_Account;
        private Korisnik m_KorisnickiNalog;

        public KorisnickiNalogViewModel()
        {
            m_Account = new Account(ApplicationContext.Current.LoginService);
        }

        public Korisnik KorisnickiNalog
        {
            get { return m_KorisnickiNalog; }
            set
            {
                if (Equals(value, m_KorisnickiNalog))
                {
                    return;
                }
                m_KorisnickiNalog = value;
                OnPropertyChanged("KorisnickiNalog");
            }
        }

        public override void Init()
        {
            var idKorisnika = ApplicationContext.Current.IdKorisnika;
            if (idKorisnika != 0)
            {
                KorisnickiNalog = m_Account.GetUserInfo(idKorisnika);
            }
        }

        public void Sacuvaj()
        {
            m_Account.Save(KorisnickiNalog);
        }
    }
}