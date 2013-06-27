using System;
using System.ComponentModel;
using RVMS.Model.DTO;
using RVMS.Model.Entities;
using RVMS.Win.RvmsServices;

namespace RVMS.Win.ViewModels
{
    public class StajalistaViewModel : ViewModel
    {
        private int? m_IdOpstine;
        private int? m_IdMesta;
        private string m_NazivStajalista;
        private bool m_Stanica;
        private BindingList<StajalisteDTO> m_Stajalista;
        private Opstina[] m_Opstine;
        private Mesto[] m_Mesta;

        public StajalistaViewModel()
        {
            Stajalista = new BindingList<StajalisteDTO>();
            Opstine = new Opstina[0];
            Mesta = new Mesto[0];
        }

        public int? IdOpstine
        {
            get { return m_IdOpstine; }
            set
            {
                if (value == m_IdOpstine)
                {
                    return;
                }
                m_IdOpstine = value;
                OnPropertyChanged("IdOpstine");
            }
        }

        public int? IdMesta
        {
            get { return m_IdMesta; }
            set
            {
                if (value == m_IdMesta)
                {
                    return;
                }
                m_IdMesta = value;
                OnPropertyChanged("IdMesta");
            }
        }

        public string NazivStajalista
        {
            get { return m_NazivStajalista; }
            set
            {
                if (value == m_NazivStajalista)
                {
                    return;
                }
                m_NazivStajalista = value;
                OnPropertyChanged("NazivStajalista");
            }
        }

        public bool Stanica
        {
            get { return m_Stanica; }
            set
            {
                if (value.Equals(m_Stanica))
                {
                    return;
                }
                m_Stanica = value;
                OnPropertyChanged("Stanica");
            }
        }

        public BindingList<StajalisteDTO> Stajalista
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

        public Mesto[] Mesta
        {
            get { return m_Mesta; }
            set
            {
                if (Equals(value, m_Mesta))
                {
                    return;
                }
                m_Mesta = value;
                OnPropertyChanged("Mesta");
            }
        }

        public override void Init()
        {
           UcitajOpstine();
        }

        public void UcitajOpstine()
        {
            using (var svc = new RvmsServiceClient())
            {
                IsBusy = true;
                svc.VratiOpstineCompleted += (s, e) =>
                {
                    IsBusy = false;
                    HandleError(e);
                    Opstine = e.Result;
                };
                svc.VratiOpstineAsync();
            }
        }

        internal void UcitajStajalista()
        {
            IsBusy = true;
            using (var svc = new RvmsServiceClient())
            {
                svc.VratiStajalistaMestaIOpstineAsync(IdOpstine, IdMesta);
                svc.VratiStajalistaMestaIOpstineCompleted += (s, e) =>
                {
                    IsBusy = false;
                    HandleError(e);
                    Stajalista.RaiseListChangedEvents = false;
                    Stajalista.Clear();
                    foreach (var stajaliste in e.Result)
                    {
                        Stajalista.Add(stajaliste);
                    }
                    Stajalista.RaiseListChangedEvents = true;
                    OnPropertyChanged("Stajalista");
                };
            }
        }

        internal void UcitajMesta()
        {
            using (var svc = new RvmsServiceClient())
            {
                IsBusy = true;
                svc.VratiMestaCompleted += (s, e) =>
                {
                    IsBusy = false;
                    HandleError(e);
                    Mesta = e.Result;
                };
               svc.VratiMestaAsync(IdOpstine);
            }
        }

        public string Dodaj()
        {
            if (!IdOpstine.HasValue)
            {
                return "Opština je obavezna";
            }
            if (string.IsNullOrEmpty(NazivStajalista)) return "Unesite naziv stajališta";
            using (var svc = new RvmsServiceClient())
            {
                var id = svc.SacuvajStajaliste(new Stajaliste()
                {
                    Naziv = NazivStajalista,
                    OpstinaId = IdOpstine.Value,
                    MestoId = IdMesta,
                    Aktivan = true,
                    Stanica = Stanica,
                    Novo = true
                });
                NazivStajalista = null;
                Stanica = false;
            }
            return null;
        }

        public void Update(StajalisteDTO stajaliste)
        {
            using (var svc = new RvmsServiceClient())
            {
                svc.SacuvajStajaliste(new Stajaliste()
                {
                    Id = stajaliste.Id,
                    Naziv = stajaliste.Naziv,
                    OpstinaId = stajaliste.OpstinaId,
                    MestoId = stajaliste.MestoId,
                    GpsLatituda = stajaliste.Latituda,
                    GpsLongituda = stajaliste.Longituda,
                    Stanica = stajaliste.Stanica,
                    Aktivan = true
                });
            }
        }

        public void Obrisi(StajalisteDTO stajaliste)
        {
            using (var svc = new RvmsServiceClient())
            {
                var msg = svc.ObrisiStajaliste(stajaliste.Id);
                if (msg != null) throw new Exception(msg);
                Stajalista.Remove(stajaliste);
            }
        }
    }
}