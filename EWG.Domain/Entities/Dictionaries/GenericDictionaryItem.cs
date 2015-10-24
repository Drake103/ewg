using System;

namespace EWG.Domain.Entities.Dictionaries
{
    public abstract class GenericDictionaryItem : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual string ResourceName { get; set; }
        public virtual string PublicCode { get; set; }
        public virtual DateTime DateStart { get; set; }
        public virtual DateTime? DateEnd { get; set; }
    }
}