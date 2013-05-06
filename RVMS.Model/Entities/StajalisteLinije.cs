using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RVMS.Model.Entities
{
    public class StajalisteLinije : Entity
    {
        public int Rbr { get; set; }

        [ForeignKey("LinijaId")]
        public Linija Linija { get; set; }

        public int LinijaId { get; set; }

        public Stajaliste Stajaliste { get; set; }

        public int StajalisteId { get; set; }

        public decimal Rastojanje { get; set; }
    }
}