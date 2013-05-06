namespace RVMS.Model.Entities
{
    public class Stajaliste : Entity
    {
        public string Naziv { get; set; }

        public Opstina Opstina { get; set; }
        public int OpstinaId { get; set; }

        public int? MestoId { get; set; }
        public Mesto Mesto { get; set; }

        public decimal? GpsLatituda { get; set; }
        public decimal? GpsLongituda { get; set; }

        public bool Stanica { get; set; }
    }
}