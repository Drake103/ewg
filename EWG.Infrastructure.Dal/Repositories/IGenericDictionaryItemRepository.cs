using System.Collections.Generic;
using EWG.Domain.Entities.Dictionaries;

namespace EWG.Infrastructure.Dal.Repositories
{
    public interface IGenericDictionaryItemRepository<TGenericDictionaryItem>
        : IGenericRepository<TGenericDictionaryItem> where TGenericDictionaryItem : GenericDictionaryItem
    {
        TGenericDictionaryItem GetByPublicCode(string publicCode);
        IList<TGenericDictionaryItem> GetActual();
    }
}