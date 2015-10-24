namespace EWG.Domain.Entities
{
    public class Image : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual string Url { get; set; }
    }
}