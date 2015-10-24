using System;
using System.Collections.Generic;
using EWG.Domain.Entities;
using EWG.Shared.Dto;

namespace EWG.Infrastructure.Dal.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : EntityBase
    {
        IList<TEntity> Get(PagingInfo pagingInfo = PagingInfo.All);
        TEntity GetById(int id);
        void Save(TEntity entity);
        void Delete(TEntity entity);
        int GetTotalCount();
    }
}