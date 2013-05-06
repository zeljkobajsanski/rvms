using System.ComponentModel.DataAnnotations;

namespace RVMS.Model.Entities
{
    public class Mesto : Entity
    {
        [Required(ErrorMessage = "Naziv mesto nije unet")]
        public string Naziv { get; set; }

        public int OpstinaId { get; set; }
        public Opstina Opstina { get; set; }
    }
}