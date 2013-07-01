using System.Collections.Generic;
using RVMS.Model.Entities;

namespace RVMS.Model.Repository.Interfaces
{
    public interface ILinijeRepository : IRepository<Linija>
    {
        IEnumerable<StajalisteLinije> VratiStajalistaLinije(int idLinije);
        Linija UcitajLinijuIStajalista(int id);
        int? VratiIdPoslednjegStajalistaNaLiniji(int idLinije);
    }
}