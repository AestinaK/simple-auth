namespace simple_authentication.Manager.Interface
{
    public interface IAuthManager
    {
        Task login(string username, string password);
        Task logout();
    }
}
