using RVMS.Model.DTO;
using RVMS.Model.Entities;
using RVMS.Win.Models.Validators;
using System.Linq;
using RVMS.Win.RvmsServices;
using RelacijaSaMedjustanicnimRastojanjimaDTO = RVMS.Model.DTO.RelacijaSaMedjustanicnimRastojanjimaDTO;

namespace RVMS.Win.ViewModels
{
    public class RelacijaViewModel : ViewModel
    {
        private Opstina[] m_Opstine;
        private int? m_IdPolaznogStajalista;
        private int? m_IdPolazneOpstine;
        private int? m_IdDolazneOpstine;
        private int? m_IdDolaznogStajalista;
        private RelacijaSaMedjustanicnimRastojanjimaDTO fRelacija;
        private string fNazivRelacije;
        private readonly RelacijeViewModelValidator m_ModelValidator;
        private MedjustanicnoRastojanjeDTO[] fMedjustanicnaRastojanja;

        public RelacijaViewModel()
        {
            Relacija = new RelacijaSaMedjustanicnimRastojanjimaDTO();
            m_ModelValidator = new RelacijeViewModelValidator();
        }

        public string NazivRelacije
        {
            get { return fNazivRelacije; }
            set
            {
                if (NazivRelacije == value) return;
                fNazivRelacije = value;
                OnPropertyChanged("NazivRelacije");
            }
        }

        public Opstina[] Opstine
        {
            get { return m_Opstine; }
            set
            {
                if (Equals(value, m_Opstine))
                {
                    return;
                }
                m_Opstine = value;
                OnPropertyChanged("Opstine");
            }
        }

        public int? IdPolazneOpstine
        {
            get { return m_IdPolazneOpstine; }
            set
            {
                if (value == m_IdPolazneOpstine)
                {
                    return;
                }
                m_IdPolazneOpstine = value;
                OnPropertyChanged("IdPolazneOpstine");
            }
        }

        public int? IdPolaznogStajalista
        {
            get { return m_IdPolaznogStajalista; }
            set
            {
                if (IdPolaznogStajalista == value) return;
                m_IdPolaznogStajalista = value;
                OnPropertyChanged("IdPolaznogStajalista");
            }
        }

        public int? IdDolazneOpstine
        {
            get { return m_IdDolazneOpstine; }
            set
            {
                if (value == m_IdDolazneOpstine)
                {
                    return;
                }
                m_IdDolazneOpstine = value;
                OnPropertyChanged("IdDolazneOpstine");
            }
        }

        public int? IdDolaznogStajalista
        {
            get { return m_IdDolaznogStajalista; }
            set
            {
                if (value == m_IdDolaznogStajalista)
                {
                    return;
                }
                m_IdDolaznogStajalista = value;
                OnPropertyChanged("IdDolaznogStajalista");
            }
        }

        public decimal Razdaljina { get; set; }

        public int VremeVoznje { get; set; }

        public StajalisteDTO[] PolaznaStajalista { get; set; }
        
        public StajalisteDTO[] DolaznaStajalista { get; set; }

        public RelacijaSaMedjustanicnimRastojanjimaDTO Relacija
        {
            get { return fRelacija; }
            set
            {
                fRelacija = value;
                if (Relacija != null)
                {
                    NazivRelacije = Relacija.NazivRelacije;
                }
                OnPropertyChanged("Relacija");
            }
        }

        public MedjustanicnoRastojanjeDTO[] MedjustanicnaRastojanja
        {
            get { return fMedjustanicnaRastojanja; }
            set
            {
                if (Equals(value, fMedjustanicnaRastojanja)) return;
                fMedjustanicnaRastojanja = value;
                OnPropertyChanged("MedjustanicnaRastojanja");
            }
        }

        public override void Init()
        {
            using (var svc = new RvmsServiceClient())
            {
                Opstine = svc.VratiOpstine();
            }
        }

        public void UcitajPolazneStanice()
        {
            using (var svc = new RvmsServiceClient())
            {
                PolaznaStajalista = svc.VratiStajalisteOpstine(IdPolazneOpstine);
            }
        }

        public void UcitajDolazneStanice()
        {
            using (var svc = new RvmsServiceClient())
            {
                DolaznaStajalista = svc.VratiStajalisteOpstine(IdDolazneOpstine);
            }
        }

        public void UcitajRelaciju(int idRelacije)
        {
            using (var svc = new RvmsServiceClient())
            {
                var result = svc.VratiRelacijuSaRastojanjima(idRelacije);
                if (result != null)
                {
                    Relacija = result;
                    MedjustanicnaRastojanja = result.Stanice;
                }
            }
        }

        public void Sacuvaj()
        {
            using (var svc = new RvmsServiceClient())
            {
                var id = svc.SacuvajRelaciju(new Relacija()
                {
                    Id = Relacija.IdRelacije,
                    Naziv = NazivRelacije,
                    Aktivan = true
                });
                Relacija.IdRelacije = id;
            }
        }

        public override string this[string columnName]
        {
            get
            {
                var r = m_ModelValidator.Validate(this);
                var fe = r.Errors.FirstOrDefault(x => x.PropertyName == columnName);
                return fe != null ? fe.ErrorMessage : null;
            }
        }

        public override bool IsValid
        {
            get
            {
                return m_ModelValidator.Validate(this).IsValid;
            }
        }

        public void NoviUnos()
        {
            Relacija = new RelacijaSaMedjustanicnimRastojanjimaDTO();
            IdPolazneOpstine = null;
            IdPolaznogStajalista = null;
            IdDolazneOpstine = null;
            IdDolaznogStajalista = null;
        }

        public void Dodaj()
        {
            if (IdPolaznogStajalista.HasValue && IdDolaznogStajalista.HasValue)
            {
                using (var svc = new RvmsServiceClient())
                {
                    MedjustanicnaRastojanja = svc.SacuvajRastojanje(new MedjustanicnoRastojanje()
                    {
                        RelacijaId = Relacija.IdRelacije,
                        PolaznoStajalisteId = IdPolaznogStajalista.Value,
                        DolaznoStajalisteId = IdDolaznogStajalista.Value,
                        Rastojanje = Razdaljina,
                        VremeVoznje = VremeVoznje,
                        Aktivan = true
                    });
                    IdPolazneOpstine = IdDolazneOpstine;
                    IdPolaznogStajalista = IdDolaznogStajalista;
                    IdDolaznogStajalista = null;
                }
            }
        }
    }
}