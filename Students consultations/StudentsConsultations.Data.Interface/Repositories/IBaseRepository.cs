using StudentsConsultations.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace StudentsConsultations.Data.Interface.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll();

        TEntity GetById(int id);
    }
}
