using System.Collections.Generic;
using System.ComponentModel;
using RVMS.Model.DTO;
using RVMS.Win.RvmsServices;
using System.Linq;

namespace RVMS.Win.ViewModels
{
    public class DaljinarViewModel : ViewModel
    {
        private List<RelacijaDTO> m_Daljinar;
        private StajalisteDTO[] fStajalista;
        private int? fIzabranoStajaliste;
        private int fTipStajalista;
        private Dictionary<int, string> m_Tooltips = new Dictionary<int, string>();

        public DaljinarViewModel()
        {
            Daljinar = new List<RelacijaDTO>();
            Stajalista = new StajalisteDTO[0];
            TipStajalista = 1;
        }
        
        public List<RelacijaDTO> Daljinar
        {
            get { return m_Daljinar; }
            set
            {
                if (Equals(value, m_Daljinar))
                {
                    return;
                }
                m_Daljinar = value;
                OnPropertyChanged("Daljinar");
            }
        }

        public StajalisteDTO[] Stajalista
        {
            get { return fStajalista; }
            set
            {
                if (Equals(value, fStajalista)) return;
                fStajalista = value;
                OnPropertyChanged("Stajalista");
            }
        }

        public int? IzabranoStajaliste
        {
            get { return fIzabranoStajaliste; }
            set
            {
                if (IzabranoStajaliste == value) return;
                fIzabranoStajaliste = value;
                OnPropertyChanged("IzabranoStajaliste");
            }
        }

        public int TipStajalista
        {
            get { return fTipStajalista; }
            set
            {
                if (value == fTipStajalista) return;
                fTipStajalista = value;
                OnPropertyChanged("TipStajalista");
            }
        }

        public void UcitajDaljinar()
        {
            IsBusy = true;
            using (var svc = new RvmsServiceClient())
            {
                svc.VratiDaljinarCompleted += (s, e) =>
                {
                    IsBusy = false;
                    HandleError(e);
                    Daljinar = e.Result.ToList();
                };
                svc.VratiDaljinarAsync(TipStajalista, IzabranoStajaliste);
            }
        }

        public void ObrisiRelaciju(int id)
        {
            using (var svc = new RvmsServiceClient())
            {
                svc.ObrisiRelaciju(id);
            }
        }

        public void UcitajStajalista()
        {
            using (var svc = new RvmsServiceClient())
            {
                IsBusy = true;
                svc.VratiStajalistaMestaIOpstineCompleted += (s, e) =>
                {
                    HandleError(e);
                    Stajalista = e.Result.OrderBy(x => x.Naziv).ThenBy(x => x.Opstina).ToArray();
                    IsBusy = false;
                };
                svc.VratiStajalistaMestaIOpstineAsync(null, null);
            }
        }

        public string VratiTooltip(int idRelacije)
        {
            if (m_Tooltips.ContainsKey(idRelacije))
            {
                return m_Tooltips[idRelacije];
            }
            using (var svc = new RvmsServiceClient())
            {
                var tooltip = svc.VratiTooltipZaRelaciju(idRelacije);
                m_Tooltips.Add(idRelacije, tooltip);
                return tooltip;
            }
        }
    }
}