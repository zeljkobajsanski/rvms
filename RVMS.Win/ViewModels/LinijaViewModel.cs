using System;
using System.ComponentModel;
using FluentValidation;
using FluentValidation.Results;
using RVMS.Model.DTO;
using RVMS.Win.Models;
using RVMS.Win.Models.Validators;
using RVMS.Win.RvmsServices;
using System.Linq;
using RVMS.Win.Services.Linije;
using RVMS.Win.Services.Stajalista;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace RVMS.Win.ViewModels
{
    public class LinijaViewModel : ViewModel
    {
        private readonly BindingList<StajalisteLinije> m_StajalistaLinije = new BindingList<StajalisteLinije>();
        
        private StajalisteDTO[] m_Stajalista;
        private RelacijaDTO[] m_Relacije;
        private StajalisteDTO[] m_DodataStajalista;
        private string fNazivLinije;
        private int? fIdPrevoznika;
        private readonly LinijaViewModelValidator fModelValidator = new LinijaViewModelValidator();
        private int fIdLinije;

        public LinijaViewModel()
        {
            IdPrevoznika = 1;
        }

        public override void Init()
        {
            using (var svc = new StajalistaClient())
            {
                svc.VratiStajalistaCompleted += (s, e) =>
                {
                    HandleError(e);
                    Stajalista = e.Result.OrderBy(x => x.Naziv).ThenBy(x => x.Opstina).ToArray();
                };
                svc.VratiStajalistaAsync(null, null);
            }
        }

        public BindingList<StajalisteLinije> StajalistaLinije { get { return m_StajalistaLinije; } }

        public int IdLinije
        {
            get { return fIdLinije; }
            set
            {
                if (value == fIdLinije) return;
                fIdLinije = value;
                OnPropertyChanged("IdLinije");
            }
        }

        public string NazivLinije
        {
            get { return fNazivLinije; }
            set
            {
                if (value == fNazivLinije) return;
                fNazivLinije = value;
                OnPropertyChanged("NazivLinije");
            }
        }

        public int? IdPrevoznika
        {
            get { return fIdPrevoznika; }
            set
            {
                if (value == fIdPrevoznika) return;
                fIdPrevoznika = value;
                OnPropertyChanged("IdPrevoznika");
            }
        }

        public StajalisteDTO[] Stajalista
        {
            get { return m_Stajalista; }
            set
            {
                if (Equals(value, m_Stajalista))
                {
                    return;
                }
                m_Stajalista = value;
                OnPropertyChanged("Stajalista");
            }
        }

        public StajalisteDTO[] DodataStajalista
        {
            get { return m_DodataStajalista; }
            set
            {
                if (Equals(value, m_DodataStajalista))
                {
                    return;
                }
                m_DodataStajalista = value;
                OnPropertyChanged("DodataStajalista");
            }
        }

        public RelacijaDTO[] Relacije
        {
            get { return m_Relacije; }
            set
            {
                if (Equals(value, m_Relacije))
                {
                    return;
                }
                m_Relacije = value;
                OnPropertyChanged("Relacije");
            }
        }

        public void DodajStajaliste(int idStajalista)
        {
            using (var svc = new LinijeClient())
            {
                svc.DodajStajalisteNaLinijuCompleted += (s, e) =>
                {
                    HandleError(e);
                    Stajalista = e.Result.Stajalista;
                    Relacije = e.Result.Relacije.ToArray();
                    m_StajalistaLinije.Clear();
                    foreach (var stajaliste in e.Result.Linija.Stajalista)
                    {
                        m_StajalistaLinije.Add(new StajalisteLinije
                        {
                            Id = stajaliste.Id,
                            IdStajalista = stajaliste.StajalisteId,
                            NazivStajalista = stajaliste.NazivStajalista, 
                            Rastojanje = stajaliste.Rastojanje
                        });
                    }
                };
                svc.DodajStajalisteNaLinijuAsync(IdLinije, idStajalista);
            }
        }

        public void DodajStajalistaRelacije(int idRelacije)
        {
            using (var svc = new LinijeClient())
            {
                svc.DodajStajalistaRelacijeNaLinijuCompleted += (s, e) =>
                {
                    HandleError(e);
                    Stajalista = e.Result.Stajalista;
                    Relacije = e.Result.Relacije.ToArray();
                    foreach (var stajalisteLinije in e.Result.Linija.Stajalista)
                    {
                        m_StajalistaLinije.Add(new StajalisteLinije
                        {
                            Id = stajalisteLinije.Id,
                            IdStajalista = stajalisteLinije.Id,
                            NazivStajalista = stajalisteLinije.NazivStajalista
                        });
                    }
                };
                svc.DodajStajalistaRelacijeNaLinijuAsync(IdLinije, idRelacije);
            }
        }

        public void NoviUnos()
        {
            m_StajalistaLinije.Clear();
            Init();
        }

        public void Obrisi(StajalisteLinije stajaliste)
        {
            
            using (var svc = new LinijeClient())
            {
                svc.SkloniStajalisteSaLinijeCompleted += (sender, args) =>
                {
                    HandleError(args);
                    Stajalista = args.Result.Stajalista;
                    Relacije = args.Result.Relacije.ToArray();
                    m_StajalistaLinije.Clear();
                    foreach (var stajalisteLinije in args.Result.Linija.Stajalista)
                    {
                        m_StajalistaLinije.Add(new StajalisteLinije
                        {
                            Id = stajalisteLinije.Id,
                            IdStajalista = stajaliste.IdStajalista,
                            NazivStajalista = stajaliste.NazivStajalista,
                            Rastojanje = stajaliste.Rastojanje
                        });
                    }
                };
                svc.SkloniStajalisteSaLinijeAsync(IdLinije, stajaliste.Id);
            }
        }

        public override string this[string columnName]
        {
            get
            {
                var r = fModelValidator.Validate(this);
                var fe = r.Errors.FirstOrDefault(x => x.PropertyName == columnName);
                return fe != null ? fe.ErrorMessage : null;
            }
        }

        public override string Error
        {
            get
            {
                return fModelValidator.Validate(this).IsValid ? null : "Linija nije validna";
            }
        }

        public void Sacuvaj()
        {
            if (Error != null)
            {
                throw new ValidationException();
            }
            using (var svc = new LinijeClient())
            {
                IdLinije = svc.SacuvajLiniju(new LinijaDTO()
                {
                    Id = 0,
                    PrevoznikId = IdPrevoznika ?? 0,
                    Naziv = NazivLinije
                });
            }
        }
    }
}