using System.Linq;
using RVMS.Model.Entities;

namespace RVMS.Model.Repository.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(int id);
        IQueryable<T> GetAll();
        void Dispose();
        void Save();
        IQueryable<T> GetActive();
    }
}