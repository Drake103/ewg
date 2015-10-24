using System;
using System.Linq;
using EWG.Domain.Entities;
using NHibernate;
using NHibernate.Linq;

namespace EWG.Infrastructure.Dal.Impl
{
    public class CrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity : EntityBase
    {
        private readonly IUnitOfWork _uow;

        public CrudRepository(IUnitOfWork uow)
        {
            _uow = uow;
            var nhUow = uow as NhUnitOfWork;
            if (nhUow == null)
            {
                throw new NotSupportedException();
            }

            _session = nhUow.GetSession();
        }

        private readonly ISession _session;

        public TEntity GetById(int id)
        {
            return _session.Get<TEntity>(id);
        }

        public void Save(TEntity entity)
        {
            using (var tx = _uow.BeginTransaction())
            {
                _session.SaveOrUpdate(entity);
                tx.Commit();
            }
            
        }

        public void Delete(TEntity entity)
        {
            using (var tx = _uow.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();
            }
        }

        public void Detach(TEntity entity)
        {
            _session.Evict(entity);
        }

        public IQueryable<TEntity> Get()
        {
            return _session.Query<TEntity>();
        }
    }
}