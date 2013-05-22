using System;
using System.Collections.Generic;
using System.Data;
using RVMS.Model.Entities;
using System.Linq;

namespace RVMS.Model.Repository
{
    public class Repository<T> where T : Entity
    {

        protected readonly DataContext fDataContext = new DataContext();

        public void Add(T entity)
        {
            fDataContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            fDataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            fDataContext.Entry(entity).State = EntityState.Deleted;
        }

        public T Get(int id)
        {
            return fDataContext.Set<T>().SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return fDataContext.Set<T>();
        } 

        public void Dispose()
        {
            fDataContext.Dispose();
        }

        public void Save()
        {
            fDataContext.SaveChanges();
        }

        public void MarkUnchanged(Entity entity)
        {
            fDataContext.Entry(entity).State = EntityState.Unchanged;
        }

        public IQueryable<T> GetActive()
        {
            return fDataContext.Set<T>().Where(x => x.Aktivan);
        } 
    }
}