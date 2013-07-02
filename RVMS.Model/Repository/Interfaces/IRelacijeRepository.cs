using System.Collections.Generic;
using System.Linq;
using RVMS.Model.Entities;

namespace RVMS.Model.Repository.Interfaces
{
    public interface IRelacijeRepository : IRepository<Relacija>
    {
        IQueryable<Relacija> VratiRelacije(int tipStajalista, int? idStajalista);
        IQueryable<Relacija> VratiRelacijeSaRastojanjima();
        IQueryable<Relacija> VratiRelacijeKojePolazeSaStanice(int idStajalista);
        IQueryable<Relacija> VratiRelacijeKojeProlazeKrozStanicu(int idStajalista);
        Relacija VratiRelacijuSaRastojanjima(int idRelacije);
        bool PostojiRelacijaSaStajalistem(int idStajalista);
    }
}