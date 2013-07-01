using System.Collections.Generic;

namespace RVMS.Model.DTO
{
    public class LinijaDTO
    {
        public LinijaDTO()
        {
            Stajalista = new List<StajalisteLinijeDTO>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public int PrevoznikId { get; set; }
        public List<StajalisteLinijeDTO> Stajalista { get; set; }
    }
}