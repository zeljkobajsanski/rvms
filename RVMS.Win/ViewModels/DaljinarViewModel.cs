using System.ComponentModel;
using RVMS.Model.DTO;
using RVMS.Win.RvmsServices;

namespace RVMS.Win.ViewModels
{
    public class DaljinarViewModel : ViewModel
    {
        private RelacijaDTO[] m_Daljinar;

        public DaljinarViewModel()
        {
            Daljinar = new RelacijaDTO[0];
        }
        
        public RelacijaDTO[] Daljinar
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
                    Daljinar = e.Result;
                };
                svc.VratiDaljinarAsync();
            }
        }
    }
}