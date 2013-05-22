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

        public StajalistaViewModel()
        {
            Stajalista = new BindingList<StajalisteDTO>();
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

        public BindingList<StajalisteDTO> Stajalista { get; set; }

        public Opstina[] Opstine { get; set; }

        public Mesto[] Mesta { get; set; }

        public override void Init()
        {
           UcitajOpstine();
        }

        public void UcitajOpstine()
        {
            using (var svc = new RvmsServiceClient())
            {
                Opstine = svc.VratiOpstine();
            }
        }

        internal void UcitajStajalista()
        {
            using (var svc = new RvmsServiceClient())
            {
                Stajalista.Clear();
                var stajalista = svc.VratiStajalistaMestaIOpstine(IdOpstine, IdMesta);
                foreach (var stajalisteDto in stajalista)
                {
                    Stajalista.Add(stajalisteDto);
                }
            }
        }

        internal void UcitajMesta()
        {
            using (var svc = new RvmsServiceClient())
            {
               Mesta = svc.VratiMesta(IdOpstine);
            }
        }
    }
}