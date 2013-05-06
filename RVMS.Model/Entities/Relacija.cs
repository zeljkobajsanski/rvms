using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RVMS.Model.Entities
{
    public class Relacija : Entity
    {
        public Relacija()
        {
            MedjustanicnaRastojanja = new List<MedjustanicnoRastojanje>();
        }

        [Required(ErrorMessage = "Naziv relacije nije unet")]
        public string Naziv { get; set; }

        public IList<MedjustanicnoRastojanje> MedjustanicnaRastojanja { get; set; }

        public decimal DuzinaRelacije { get { return MedjustanicnaRastojanja.Sum(x => x.Rastojanje); } }

        public int VremeVoznje { get { return MedjustanicnaRastojanja.Sum(x => x.VremeVoznje); } }

        public decimal SrednjaSaobracajnaBrzina
        {
            get
            {
                if (VremeVoznje == 0) return 0;
                return Math.Round(DuzinaRelacije/((decimal)VremeVoznje/60), 2);
            }
        }
    }
}