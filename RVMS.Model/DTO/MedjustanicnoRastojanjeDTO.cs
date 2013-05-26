namespace RVMS.Model.DTO
{
    public class MedjustanicnoRastojanjeDTO
    {
        public int Id { get; set; }
        public int Rbr { get; set; }
        public int PolaznoStajalisteId { get; set; }
        public string PolaznoStajaliste { get; set; }
        public int DolaznoStajalisteId { get; set; }
        public string DolaznoStajaliste { get; set; }
        public decimal Rastojanje { get; set; }
        public decimal DuzinaRelacije { get; set; }
        public int VremeVoznje { get; set; }
        public int VremeVoznjePoRelaciji { get; set; }
        public decimal? LatitudaPolaznogStajalista { get; set; }
        public decimal? LongitudaPolaznogStajalista { get; set; }
        public decimal? LatitudaDolaznogStajalista { get; set; }
        public decimal? LongitudaDolaznogStajalista { get; set; }
    }
}