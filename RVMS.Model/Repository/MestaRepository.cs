using System.Collections.Generic;
using RVMS.Model.Entities;
using System.Linq;
using RVMS.Model.Repository.Interfaces;

namespace RVMS.Model.Repository
{
    public class MestaRepository : Repository<Mesto>, IMestaRepository
    {
        public MestaRepository()
        {
        }

        public MestaRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public IQueryable<Mesto> VratiMestaOpstine(int? idOpstine)
         {
             var mesta = GetActive();
             if (idOpstine.HasValue)
             {
                 mesta = mesta.Where(x => x.OpstinaId == idOpstine);
             }
             return mesta;
         } 
    }
}