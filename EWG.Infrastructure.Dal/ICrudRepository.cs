using System.Linq;
using EWG.Domain.Entities;

namespace EWG.Infrastructure.Dal
{
    public interface ICrudRepository<TEntity> where TEntity : EntityBase
    {
        TEntity GetById(int id);
        void Save(TEntity entity);
        void Delete(TEntity entity);
        void Detach(TEntity entity);
        IQueryable<TEntity> Get();
    }
}