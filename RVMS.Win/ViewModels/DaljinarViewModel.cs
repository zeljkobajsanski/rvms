using RVMS.Model.DTO;
using RVMS.Win.RvmsServices;

namespace RVMS.Win.ViewModels
{
    public class DaljinarViewModel : ViewModel
    {
        private RelacijaDTO[] m_Daljinar;
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
            using (var svc = new RvmsServiceClient())
            {
                Daljinar = svc.VratiDaljinar();
            }
        }
    }
}