using System;
using System.ComponentModel;
using RVMS.Model.DTO;
using RVMS.Win.Models;
using RVMS.Win.RvmsServices;
using System.Linq;
using RVMS.Win.Services.Linije;
using RVMS.Win.Services.Stajalista;

namespace RVMS.Win.ViewModels
{
    public class LinijaViewModel : ViewModel
    {
        private readonly BindingList<StajalisteLinije> m_StajalistaLinije = new BindingList<StajalisteLinije>();
        
        private StajalisteDTO[] m_Stajalista;
        private RelacijaDTO[] m_Relacije;
        private StajalisteDTO[] m_DodataStajalista;

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
            var stajaliste = m_Stajalista.SingleOrDefault(x => x.Id == idStajalista);
            if (stajaliste == null) throw new Exception("Stajalište id: " + idStajalista + " nije pronađeno");
            m_StajalistaLinije.Add(new StajalisteLinije
            {
                IdStajalista = idStajalista,
                NazivStajalista = stajaliste.Naziv
            });
            using (var svc = new LinijeClient())
            {
                svc.DodajStajalisteNaLinijuCompleted += (s, e) =>
                {
                    HandleError(e);
                    Stajalista = e.Result.Stajalista;
                    Relacije = e.Result.Relacije.ToArray();
                };
                svc.DodajStajalisteNaLinijuAsync(1, idStajalista); //TODO: IdLinije
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
                            IdStajalista = stajalisteLinije.Id,
                            NazivStajalista = stajalisteLinije.Naziv
                        });
                    }
                    DodataStajalista = e.Result.Linija.Stajalista.ToArray();
                };
                svc.DodajStajalistaRelacijeNaLinijuAsync(1, idRelacije); //TODO: IdLinije
            }
        }

        public void NoviUnos()
        {
            m_StajalistaLinije.Clear();
            Init();
        }

        public void Obrisi(StajalisteLinije stajaliste)
        {
            var last = m_StajalistaLinije.LastOrDefault();
            if (last == null) return;
            if (stajaliste != last)
            {
                throw new Exception("Dozvoljeno je obrisati samo poslednje stajalište na liniji");
            }
            m_StajalistaLinije.Remove(stajaliste);
            last = m_StajalistaLinije.LastOrDefault();
            if (last != null)
            {
                using (var svc = new LinijeClient())
                {
                    svc.SkloniStajalisteSaLinijeCompleted += (sender, args) =>
                    {
                        HandleError(args);
                        Stajalista = args.Result.Stajalista;
                    };
                    svc.SkloniStajalisteSaLinijeAsync(1, last.IdStajalista); //TODO: IdLinije
                }
            }
            else
            {
                using (var svc = new StajalistaClient())
                {
                    svc.VratiStajalistaCompleted += (s, e) =>
                    {
                        HandleError(e);
                        Stajalista = e.Result;
                    };
                    svc.VratiStajalistaAsync(null, null);
                }
            }
        }

        
    }
}