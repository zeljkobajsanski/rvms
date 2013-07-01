using System.ComponentModel.DataAnnotations.Schema;
using RVMS.Model.Entities;

namespace RVMS.Model.DTO
{
    public class StajalisteLinijeDTO
    {
        public int Id { get; set; }

        public int Rbr { get; set; }

        public int LinijaId { get; set; }

        public int StajalisteId { get; set; }

        public string NazivStajalista { get; set; }

        public decimal? Latituda { get; set; }

        public decimal? Longituda { get; set; }

        public decimal Rastojanje { get; set; } 
    }
}