using System.Collections.Generic;
using RVMS.Model.Entities;
using System.Linq;

namespace RVMS.Model.Repository
{
    public class PrevozniciRepository : Repository<Prevoznik>
    {
        public IEnumerable<Prevoznik> VratiAktivnePrevoznike()
        {
            return fDataContext.Prevoznici.Where(x => x.Aktivan).ToArray();
        }
    }
}