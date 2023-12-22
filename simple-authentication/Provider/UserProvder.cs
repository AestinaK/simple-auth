using Microsoft.AspNetCore.Authorization;
using simple_authentication.Data;
using simple_authentication.Entity;
using simple_authentication.Provider.Interface;
using System.Security.Claims;

namespace simple_authentication.Provider
{
  
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ApplicationDbContext _context;

        public UserProvider(IHttpContextAccessor accessor,
            ApplicationDbContext context)
        {
           _accessor = accessor;
           _context = context;
        }
        public async Task<User?> GetCurrentUser()
        {
            var currentUser = GetCurrentUserId();
            if(!currentUser.HasValue) return null;
                return await _context.users.FindAsync(currentUser.Value);
                    
        }

        public long? GetCurrentUserId()
        {
            var userId = _accessor.HttpContext?.User.FindFirstValue("Id");
            if (string.IsNullOrWhiteSpace(userId)) return null;
            return Convert.ToInt64(userId);
        }

        public bool IsLoggedIn()=> GetCurrentUserId() != null;
        
        
    }
}
