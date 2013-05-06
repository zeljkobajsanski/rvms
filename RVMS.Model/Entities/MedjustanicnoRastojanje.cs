namespace RVMS.Model.Entities
{
    public class MedjustanicnoRastojanje : Entity
    {
        public int Rbr { get; set; }

        public Relacija Relacija { get; set; }
        public int RelacijaId { get; set; }

        public int PolaznoStajalisteId { get; set; }
        public Stajaliste PolaznoStajaliste { get; set; }

        public int DolaznoStajalisteId { get; set; }
        public Stajaliste DolaznoStajaliste { get; set; }

        public decimal Rastojanje { get; set; }

        public int VremeVoznje { get; set; }
    }
}