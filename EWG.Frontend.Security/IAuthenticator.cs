namespace EWG.Frontend.Security
{
    public interface IAuthenticator
    {
        void SetCookie(string email, string username, bool persistent = false, string[] roles = null, byte[] tag = null);
        void SignOut();
    }
}