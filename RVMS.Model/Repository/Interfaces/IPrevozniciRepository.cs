using System.Collections.Generic;
using RVMS.Model.Entities;

namespace RVMS.Model.Repository.Interfaces
{
    public interface IPrevozniciRepository : IRepository<Prevoznik>
    {
        IEnumerable<Prevoznik> VratiAktivnePrevoznike();
    }
}