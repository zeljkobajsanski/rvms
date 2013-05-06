namespace RVMS.Model.DTO
{
    public class RelacijaDTO
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public decimal DuzinaRelacije { get; set; }
        public int VremeVoznje { get; set; }
        public decimal SrednjaSaobracajnaBrzina { get; set; }
    }
}