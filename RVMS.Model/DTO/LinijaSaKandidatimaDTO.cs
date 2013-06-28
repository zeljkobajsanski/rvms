using System.Collections.Generic;

namespace RVMS.Model.DTO
{
    public class LinijaSaKandidatimaDTO
    {
        public LinijaDTO Linija { get; set; }

        public StajalisteDTO[] Stajalista { get; set; }

        public List<RelacijaDTO> Relacije { get; set; }
    }
}