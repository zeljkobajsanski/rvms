using System.Collections.Generic;

namespace RVMS.Model.DTO
{
    public class StajalisteDTO
    {
        public int Id { get; set; }
        public int? IdStajalistaLinije { get; set; }
        public string Naziv { get; set; }
        public int OpstinaId { get; set; }
        public string Opstina { get; set; }
        public int? MestoId { get; set; }
        public string Mesto { get; set; }
        public decimal? Latituda { get; set; }
        public decimal? Longituda { get; set; }
        public decimal? Udaljenost { get; set; }
        public bool Stanica { get; set; }
        public bool Novo { get; set; }

        private sealed class IdEqualityComparer : IEqualityComparer<StajalisteDTO>
        {
            public bool Equals(StajalisteDTO x, StajalisteDTO y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }
                if (ReferenceEquals(x, null))
                {
                    return false;
                }
                if (ReferenceEquals(y, null))
                {
                    return false;
                }
                if (x.GetType() != y.GetType())
                {
                    return false;
                }
                return x.Id == y.Id;
            }

            public int GetHashCode(StajalisteDTO obj)
            {
                return obj.Id;
            }
        }

        private static readonly IEqualityComparer<StajalisteDTO> IdComparerInstance = new IdEqualityComparer();

        public static IEqualityComparer<StajalisteDTO> IdComparer
        {
            get { return IdComparerInstance; }
        }
    }
}