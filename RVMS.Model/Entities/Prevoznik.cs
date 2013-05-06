using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVMS.Model.Entities
{
    public class Prevoznik : Entity
    {
        public Prevoznik()
        {
            Linije = new List<Linija>();
        }

        [Required(ErrorMessage = "Naziv nije unet")]
        [StringLength(255, ErrorMessage = "Dužina naziva je do 255 karaktera")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Adresa nije uneta")]
        [StringLength(255, ErrorMessage = "Dužina adrese je do 255 karaktera")]
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Mesto nije uneto")]
        [StringLength(255, ErrorMessage = "Dužina mesta je do 255 karaktera")]
        public string Mesto { get; set; }

        public IList<Linija> Linije { get; set; }
    }
}