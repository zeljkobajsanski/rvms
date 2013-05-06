using System.Collections.Generic;

namespace RVMS.Model.Entities
{
    public class Linija : Entity
    {
        public Linija()
        {
            Stajalista = new List<StajalisteLinije>();
        }

        public string Naziv { get; set; }

        public IList<StajalisteLinije> Stajalista { get; set; }

        public int PrevoznikId { get; set; }

        public Prevoznik Prevoznik { get; set; }

        public string EvidencioniBroj { get; set; }

        public string Napomena { get; set; }

        public StajalisteLinije PretposledjeStajaliste()
        {
            if (Stajalista.Count <= 1) return null;
            return Stajalista[Stajalista.Count - 2];
        }
    }
}