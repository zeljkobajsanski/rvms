using RVMS.Model.DTO;
using RVMS.Model.Entities;
using RVMS.Win.RvmsServices;

namespace RVMS.Win.ViewModels
{
    public class IzmenaRelacijeViewModel : ViewModel
    {
        private StajalisteDTO[] m_Stajalista;
        private MedjustanicnoRastojanje m_MedjustanicnoRastojanje;

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

        public MedjustanicnoRastojanje MedjustanicnoRastojanje
        {
            get { return m_MedjustanicnoRastojanje; }
            set
            {
                if (Equals(value, m_MedjustanicnoRastojanje))
                {
                    return;
                }
                m_MedjustanicnoRastojanje = value;
                OnPropertyChanged("MedjustanicnoRastojanje");
            }
        }

        public override void Init()
        {
            UcitajStajalista();
        }

        public void UcitajMedjustanicnoRastojanje(int id)
        {
            using (var svc = new RvmsServiceClient())
            {
                IsBusy = true;
                svc.VratiMedjustanicnoRastojanjeCompleted += (s, e) =>
                {
                    IsBusy = false;
                    HandleError(e);
                    MedjustanicnoRastojanje = e.Result;
                };
                svc.VratiMedjustanicnoRastojanjeAsync(id);
            }
        }

        public void UcitajStajalista()
        {
            IsBusy = true;
            using (var svc = new RvmsServiceClient())
            {
                svc.VratiStajalisteOpstineCompleted += (s, e) =>
                {
                    IsBusy = false;
                    HandleError(e);
                    Stajalista = e.Result;
                };
                svc.VratiStajalisteOpstineAsync(null);
            }
        }
    }
}