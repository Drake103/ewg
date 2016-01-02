using System;
using System.Collections.Generic;
using System.Linq;
using EWG.Domain.Entities.Dictionaries;
using EWG.Infrastructure.Dal.Repositories;

namespace EWG.Infrastructure.Dal.Impl.Repositories
{
    public class GenericDictionaryItemRepository<TEntity>
        : GenericRepository<TEntity>, IGenericDictionaryItemRepository<TEntity> where TEntity : GenericDictionaryItem, new()
    {
        public GenericDictionaryItemRepository(ICrudRepository<TEntity> crud) : base(crud)
        {
        }

        public TEntity GetByPublicCode(string publicCode)
        {
            return _crud.Get().SingleOrDefault(x => x.DateEnd == null && x.PublicCode == publicCode);
        }

        public TEntity CreateByPublicCode(string publicCode)
        {
            var dicItem = new TEntity
            {
                PublicCode = publicCode,
                Name = publicCode,
                ResourceName = publicCode,
                DateStart = DateTime.Now
            };

            _crud.Save(dicItem);
            return dicItem;
        }

        public IList<TEntity> GetActual()
        {
            return _crud.Get().Where(x => x.DateEnd == null).ToList();
        }
    }
}