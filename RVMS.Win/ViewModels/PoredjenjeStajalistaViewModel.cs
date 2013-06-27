using System;
using System.Collections.Generic;
using System.ComponentModel;
using RVMS.Model.DTO;
using RVMS.Win.Models;
using RVMS.Win.RvmsServices;
using System.Linq;

namespace RVMS.Win.ViewModels
{
    public class PoredjenjeStajalistaViewModel : ViewModel
    {
        private StajalisteDTO[] m_Stajalista;
        
        private StajalisteSaRelacijama m_NovoStajaliste;

        public PoredjenjeStajalistaViewModel()
        {
            IzabranaStajalista = new BindingList<StajalisteSaRelacijama>();
        }

        public override void Init()
        {
            using (var svc = new RvmsServiceClient())
            {
                svc.VratiStajalistaMestaIOpstineCompleted += (s, e) =>
                {
                    HandleError(e);
                    Stajalista = e.Result.OrderBy(x => x.Naziv).ToArray();
                    OnPropertyChanged();
                };
                svc.VratiStajalistaMestaIOpstineAsync(null, null);
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

        public StajalisteSaRelacijama NovoStajaliste
        {
            get { return m_NovoStajaliste; }
            set
            {
                if (Equals(value, m_NovoStajaliste))
                {
                    return;
                }
                m_NovoStajaliste = value;
                OnPropertyChanged("NovoStajaliste");
            }
        }

        public BindingList<StajalisteSaRelacijama> IzabranaStajalista { get; set; }

        public void DodajStajaliste(StajalisteDTO stajaliste)
        {
            if (IzabranaStajalista.Any(x => x.Id == stajaliste.Id)) return;
            using (var svc = new RvmsServiceClient())
            {
                var result = svc.VratiStajalisteSaRelacijama(stajaliste.Id);
                var stajalisteSaRelacijama = new StajalisteSaRelacijama
                {
                    Id = result.Id, 
                    Naziv = result.Naziv, 
                    Opstina = result.Opstina,
                    Lat = result.Latitude,
                    Lon = result.Longitude,
                    Relacije = result.Relacije.Select(x => new Relacija {Id = x.Id, Naziv = x.Naziv}).ToList()
                };
                IzabranaStajalista.Add(stajalisteSaRelacijama);
                NovoStajaliste = stajalisteSaRelacijama;
            }
        }

        public void IzvadiStajaliste(StajalisteDTO stajaliste)
        {
            var s = IzabranaStajalista.SingleOrDefault(x => x.Id == stajaliste.Id);
            if (s != null)
            {
                IzabranaStajalista.Remove(s);
            }
        }

        public bool IzabranoStajaliste(StajalisteDTO stajaliste)
        {
            return IzabranaStajalista.Any(x => x.Id == stajaliste.Id);
        }

        public void Ocisti()
        {
            IzabranaStajalista.Clear();
        }

        public void ObrisiStajaliste(StajalisteSaRelacijama stajaliste)
        {
            using (var svc = new RvmsServiceClient())
            {
                var msg = svc.ObrisiStajaliste(stajaliste.Id);
                if (msg != null)
                {
                    throw new Exception(msg);
                }
                IzvadiStajaliste(new StajalisteDTO(){Id = stajaliste.Id});
            }
        }

        public void ProglasiDefaultStajaliste(StajalisteSaRelacijama stajaliste)
        {
            var stajalistaZaSvodjenje = IzabranaStajalista.Where(x => x.Id != stajaliste.Id).Select(x => x.Id).ToArray();
            using (var svc = new RvmsServiceClient())
            {
                svc.SvediStajalistaNaPodrazumevano(stajaliste.Id, stajalistaZaSvodjenje);
            }
        }
    }
}