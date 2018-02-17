using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);

        void Delete(TEntity entity);

        void Update(TEntity entity);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);
    }
}
