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

        public DaljinarViewModel()
        {
            Daljinar = new List<RelacijaDTO>();
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
                svc.VratiDaljinarAsync();
            }
        }

        public void ObrisiRelaciju(int id)
        {
            using (var svc = new RvmsServiceClient())
            {
                svc.ObrisiRelaciju(id);
            }
        }
    }
}