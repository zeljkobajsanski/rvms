using System.Linq;
using RVMS.Model.Entities;

namespace RVMS.Model.Repository.Interfaces
{
    public interface IMestaRepository : IRepository<Mesto>
    {
        IQueryable<Mesto> VratiMestaOpstine(int? idOpstine);
    }
}