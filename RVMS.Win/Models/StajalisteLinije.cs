namespace RVMS.Win.Models
{
    public class StajalisteLinije
    {
        public int Id { get; set; }

        public int StajalisteId { get; set; }

        public int LinijaId { get; set; }

        public bool PrimaPutnike { get; set; }

        public decimal Rastojanje { get; set; }

        public string NazivStajalista { get; set; }

        public decimal? Latituda { get; set; }

        public decimal? Longituda { get; set; }
    }
}