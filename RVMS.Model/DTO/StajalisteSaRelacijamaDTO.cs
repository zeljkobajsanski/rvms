namespace RVMS.Model.DTO
{
    public class StajalisteSaRelacijamaDTO
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Opstina { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public RelacijaDTO[] Relacije { get; set; }
    }
}