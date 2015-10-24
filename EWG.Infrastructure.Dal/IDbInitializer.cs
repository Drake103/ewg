namespace EWG.Infrastructure.Dal
{
    public interface IDbInitializer
    {
        void Initialize(string configurationFilePath);
    }
}