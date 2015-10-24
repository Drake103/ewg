using EWG.Domain.Entities;

namespace EWG.Infrastructure.Dal.Repositories
{
    public interface IPlayerUserRepository : IGenericRepository<PlayerUser>
    {
        PlayerUser GetPlayerUserByEugenUserId(int eugenUserId);
    }
}