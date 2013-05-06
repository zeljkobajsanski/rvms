using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace RVMS.Model.Entities
{
    public class Opstina : Entity
    {
        [Required(ErrorMessage = "Naziv opstine nije unet")]
        public string NazivOpstine { get; set; }
    }
}