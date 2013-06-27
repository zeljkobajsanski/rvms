using System.Collections.Generic;

namespace RVMS.Win.Models
{
    public class StajalisteSaRelacijama
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opstina { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lon { get; set; }
        public bool ImaKoordinate { get { return Lat.HasValue && Lon.HasValue; } }
        public List<Relacija> Relacije { get; set; }
    }
}