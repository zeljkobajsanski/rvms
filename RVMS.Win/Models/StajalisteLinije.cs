namespace RVMS.Win.Models
{
    public class StajalisteLinije
    {
        public int IdStajalista { get; set; }

        public int IdLinije { get; set; }

        public bool PrimaPutnike { get; set; }

        public decimal Rastojanje { get; set; }

        public string NazivStajalista { get; set; }
    }
}