using System.Linq;
using EWG.Domain.Entities;
using EWG.Infrastructure.Dal.Repositories;

namespace EWG.Infrastructure.Dal.Impl.Repositories
{
    public class PlayerUserRepository : GenericRepository<PlayerUser>, IPlayerUserRepository
    {
        public PlayerUserRepository(ICrudRepository<PlayerUser> crud) : base(crud)
        {
        }

        public PlayerUser GetPlayerUserByEugenUserId(int eugenUserId)
        {
            return _crud.Get().SingleOrDefault(x => x.EugenUserId == eugenUserId);
        }
    }
}