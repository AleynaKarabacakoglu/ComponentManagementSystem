using Domain.Abstract;
using Domain.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Repository
{
    public class BaseRepository<T> : IDisposable where T : class, IBase
    {

        public ComponentContext context = null;
        private DbSet<T> dbSet;

        public BaseRepository()
        {
            context = new ComponentContext();
            dbSet = context.Set<T>();
        }


        public bool Add(T entity) //Overrride edip yeni repo açabiliriz
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            entity.IsDeleted = false;

            dbSet.Add(entity);

            return context.SaveChanges() > 0;
        }

        public virtual T Find(int Id)
        {
            return dbSet.FirstOrDefault(x => x.Id == Id);
        }

        public virtual bool Delete(T entity)//soft delete
        {
            var record = Find(entity.Id);
            record.IsDeleted = true;
            return context.SaveChanges() > 0;
        }

        public virtual bool DeleteLayout(T entity)//hard delete
        {
            dbSet.Remove(entity);
            return context.SaveChanges() > 0;
        }

        public virtual bool Update(T entity)
        {
            context.SaveChanges();
            return context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }

        public DbSet<E> SetContext<E>() where E : class
        {
            return context.Set<E>();
        }
    }

}
