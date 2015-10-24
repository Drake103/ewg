namespace EWG.Infrastructure.Services.Common
{
    public interface IDatabaseService
    {
        void DropCreateAndInit(string configurationFileName);
    }
}