﻿using RVMS.Model.DTO;
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
        private MedjustanicnoRastojanjeDTO[] fMedjustanicnaRastojanja = new MedjustanicnoRastojanjeDTO[0];

        public RelacijaViewModel()
        {
            Relacija = new RelacijaSaMedjustanicnimRastojanjimaDTO();
            m_ModelValidator = new RelacijeViewModelValidator();
        }

        /// <summary>
        /// Naziv relacije
        /// </summary>
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

        /// <summary>
        /// Šifarnik opština
        /// </summary>
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

        /// <summary>
        /// Izabrana polazna opština
        /// </summary>
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

        /// <summary>
        /// Izabrano polazno stajalište
        /// </summary>
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

        /// <summary>
        /// Izabrana dolazna opština
        /// </summary>
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

        /// <summary>
        /// Izabrano dolazno stajalište
        /// </summary>
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

        /// <summary>
        /// Razdaljina
        /// </summary>
        public decimal Razdaljina { get; set; }

        /// <summary>
        /// Vreme vožnje
        /// </summary>
        public int VremeVoznje { get; set; }

        /// <summary>
        /// Polazna stajališta
        /// </summary>
        public StajalisteDTO[] PolaznaStajalista { get; set; }
        
        /// <summary>
        /// Dolazna stajališta
        /// </summary>
        public StajalisteDTO[] DolaznaStajalista { get; set; }

        /// <summary>
        /// Relacija
        /// </summary>
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

        /// <summary>
        /// Međustanična rastojanja
        /// </summary>
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

        /// <summary>
        /// Ukupna dužina relacije
        /// </summary>
        public decimal UkupnaDuzinaRelacije
        {
            get
            {
                if (!MedjustanicnaRastojanja.Any()) return 0;
                return MedjustanicnaRastojanja.Last().DuzinaRelacije;
            }
        }

        /// <summary>
        /// Ukupno vreme vožnje
        /// </summary>
        public int UkupnoVremeVoznje
        {
            get
            {
                if (!MedjustanicnaRastojanja.Any()) return 0;
                return MedjustanicnaRastojanja.Last().VremeVoznjePoRelaciji;
            }
        }

        /// <summary>
        /// Srednja saobraćajna brzina
        /// </summary>
        public decimal SrednjaSaobracajnaBrzina
        {
            get
            {
                if (UkupnoVremeVoznje == 0) return 0;
                return UkupnaDuzinaRelacije/(((decimal)UkupnoVremeVoznje)/60);
            }
        }

        public override bool IsValid
        {
            get
            {
                return m_ModelValidator.Validate(this).IsValid;
            }
        }

        public override void Init()
        {
            using (var svc = new RvmsServiceClient())
            {
                Opstine = svc.VratiOpstine();
                var svaStajalista = svc.VratiStajalisteOpstine(null);
                PolaznaStajalista = svaStajalista;
                DolaznaStajalista = svaStajalista;
            }
        }

        /// <summary>
        /// Učitava polazne stanice
        /// </summary>
        public void UcitajPolazneStanice()
        {
            using (var svc = new RvmsServiceClient())
            {
                PolaznaStajalista = svc.VratiStajalisteOpstine(IdPolazneOpstine);
            }
        }

        /// <summary>
        /// Učitava dolazne stanice
        /// </summary>
        public void UcitajDolazneStanice()
        {
            using (var svc = new RvmsServiceClient())
            {
                DolaznaStajalista = svc.VratiStajalisteOpstine(IdDolazneOpstine);
            }
        }

        /// <summary>
        /// Učitava relaciju
        /// </summary>
        /// <param name="idRelacije">Id relacije</param>
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

        /// <summary>
        /// Snima podatke
        /// </summary>
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

        /// <summary>
        /// Kreira novi unos
        /// </summary>
        public void NoviUnos()
        {
            Relacija = new RelacijaSaMedjustanicnimRastojanjimaDTO();
            IdPolazneOpstine = null;
            IdPolaznogStajalista = null;
            IdDolazneOpstine = null;
            IdDolaznogStajalista = null;
            MedjustanicnaRastojanja = new MedjustanicnoRastojanjeDTO[0];
        }

        /// <summary>
        /// Unos novog međustaničnog rastojanja
        /// </summary>
        public void Dodaj()
        {
            if (IdPolaznogStajalista.HasValue && IdDolaznogStajalista.HasValue)
            {
                if (!m_ModelValidator.Validate(this).IsValid) return;
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
                    Razdaljina = 0;
                    VremeVoznje = 0;
                }
            }
        }

        /// <summary>
        /// Snima izmene međustaničnog rastojanja
        /// </summary>
        /// <param name="rastojanje"></param>
        public void Sacuvaj(MedjustanicnoRastojanjeDTO rastojanje)
        {
            using (var svc = new RvmsServiceClient())
            {
                MedjustanicnaRastojanja = svc.SacuvajRastojanje(new MedjustanicnoRastojanje()
                {
                    Id = rastojanje.Id,
                    RelacijaId = Relacija.IdRelacije,
                    PolaznoStajalisteId = rastojanje.PolaznoStajalisteId,
                    DolaznoStajalisteId = rastojanje.DolaznoStajalisteId,
                    Rastojanje = rastojanje.Rastojanje,
                    VremeVoznje = rastojanje.VremeVoznje,
                    Aktivan = true
                });
            }
        }

        /// <summary>
        /// Briše međustanično rastojanje
        /// </summary>
        /// <param name="rastojanje"></param>
        public void Obrisi(MedjustanicnoRastojanjeDTO rastojanje)
        {
            using (var svc = new RvmsServiceClient())
            {
                MedjustanicnaRastojanja = svc.ObrisiRastojanje(rastojanje.Id);
            }
        }

        public void OsveziPolaznaStajalista()
        {
            OnPropertyChanged("IdPolazneOpstine");
        }

        public void OsveziDolaznaStajalista()
        {
            OnPropertyChanged("IdDolazneOpstine");
        }

        public void OsveziRelaciju()
        {
            if (Relacija.IdRelacije != 0)
            {
                UcitajRelaciju(Relacija.IdRelacije);
            }
        }
    }
}