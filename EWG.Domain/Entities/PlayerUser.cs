namespace EWG.Domain.Entities
{
    public class PlayerUser : EntityBase
    {
        public virtual int EugenUserId { get; set; }
        public virtual string Name { get; set; }
    }
}