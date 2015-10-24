namespace EWG.Domain.Entities.Security
{
    public class IdentityUser : EntityBase
    {
        public virtual string Email { get; set; }
        public virtual string Username { get; set; }
        public virtual string HashedPassword { get; set; }
        public virtual string TimeZone { get; set; }
    }
}