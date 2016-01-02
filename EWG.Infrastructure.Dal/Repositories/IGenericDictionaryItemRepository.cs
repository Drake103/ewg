using System.Collections.Generic;
using EWG.Domain.Entities.Dictionaries;

namespace EWG.Infrastructure.Dal.Repositories
{
    public interface IGenericDictionaryItemRepository<TGenericDictionaryItem>
        : IGenericRepository<TGenericDictionaryItem> where TGenericDictionaryItem : GenericDictionaryItem, new()
    {
        TGenericDictionaryItem GetByPublicCode(string publicCode);
        TGenericDictionaryItem CreateByPublicCode(string publicCode);
        IList<TGenericDictionaryItem> GetActual();
    }
}