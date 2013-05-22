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
                    Stanica = Stanica
                });
                NazivStajalista = null;
                Stanica = false;
            }
            return null;
        }
    }
}