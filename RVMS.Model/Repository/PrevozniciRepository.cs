using System.Collections.Generic;
using RVMS.Model.Entities;
using System.Linq;
using RVMS.Model.Repository.Interfaces;

namespace RVMS.Model.Repository
{
    public class PrevozniciRepository : Repository<Prevoznik>, IPrevozniciRepository
    {
        public PrevozniciRepository()
        {
        }

        public PrevozniciRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public IEnumerable<Prevoznik> VratiAktivnePrevoznike()
        {
            return DataContext.Prevoznici.Where(x => x.Aktivan).ToArray();
        }
    }
}