using System;
using System.Collections.Generic;
using System.Data;
using RVMS.Model.Entities;
using System.Linq;
using RVMS.Model.Repository.Interfaces;

namespace RVMS.Model.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {

        protected readonly DataContext DataContext = new DataContext();

        protected Repository(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public Repository() : this(new DataContext())
        {
        }

        public void Add(T entity)
        {
            DataContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            DataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DataContext.Entry(entity).State = EntityState.Deleted;
        }

        public T Get(int id)
        {
            return DataContext.Set<T>().SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return DataContext.Set<T>();
        } 

        public void Dispose()
        {
            DataContext.Dispose();
        }

        public void Save()
        {
            DataContext.SaveChanges();
        }

        public void MarkUnchanged(Entity entity)
        {
            DataContext.Entry(entity).State = EntityState.Unchanged;
        }

        public IQueryable<T> GetActive()
        {
            return DataContext.Set<T>().Where(x => x.Aktivan);
        } 
    }
}