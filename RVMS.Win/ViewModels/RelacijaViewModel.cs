using RVMS.Model.DTO;
using RVMS.Model.Entities;
using RVMS.Win.RvmsServices;

namespace RVMS.Win.ViewModels
{
    public class RelacijaViewModel : ViewModel
    {
        private Opstina[] m_Opstine;
        private int? m_IdPolaznogStajalista;
        private int? m_IdPolazneOpstine;
        private int? m_IdDolazneOpstine;
        private int? m_IdDolaznogStajalista;

        public RelacijaViewModel()
        {
            Relacija = new Relacija();
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

        public decimal Razdaljina { get; set; }

        public int VremeVoznje { get; set; }

        public StajalisteDTO[] PolaznaStajalista { get; set; }
        
        public StajalisteDTO[] DolaznaStajalista { get; set; }

        public Relacija Relacija { get; set; }

        public override void Init()
        {
            using (var svc = new RvmsServiceClient())
            {
                Opstine = svc.VratiOpstine();
            }
        }

        public void UcitajPolazneStanice()
        {
            using (var svc = new RvmsServiceClient())
            {
                PolaznaStajalista = svc.VratiStajalisteOpstine(IdPolazneOpstine);
            }
        }

        public void UcitajDolazneStanice()
        {
            using (var svc = new RvmsServiceClient())
            {
                DolaznaStajalista = svc.VratiStajalisteOpstine(IdDolazneOpstine);
            }
        }
    }
}