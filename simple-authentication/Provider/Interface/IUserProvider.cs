using simple_authentication.Entity;

namespace simple_authentication.Provider.Interface
{
    public interface IUserProvider
    {
        bool IsLoggedIn();
        Task<User?> GetCurrentUser();
        long? GetCurrentUserId();
    }
}
