using EWG.Infrastructure.Dal;
using EWG.Infrastructure.Services.Common;

namespace EWG.Infrastructure.Services.Impl.Common
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IDbInitializer _dbInitializer;

        public DatabaseService(IDbInitializer dbInitializer)
        {
            _dbInitializer = dbInitializer;
        }

        public void DropCreateAndInit(string configurationFileName)
        {
            _dbInitializer.Initialize(configurationFileName);
        }
    }
}