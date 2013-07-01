using System.Collections.Generic;
using System.Linq;
using RVMS.Model.Entities;

namespace RVMS.Model.Repository.Interfaces
{
    public interface IStajalistaRepository : IRepository<Stajaliste>
    {
        IQueryable<Stajaliste> VratiStajalista(int? idOpstine, int? idMesta, bool includeParents = false);
        IEnumerable<Stajaliste> VratiAktivnaStajalista();
        Stajaliste VratiStajalisteIOpstinu(int idStajalista);
        IEnumerable<Stajaliste> PretraziStajalista(int? idOpstine, int? idMesta, string nazivStajalista);
        IQueryable<Stajaliste> VratiStajalistaOpstine(int? idOpstine);
    }
}