using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using simple_authentication.Data;
using simple_authentication.Manager.Interface;
using System.Security.Claims;

namespace simple_authentication.Manager
{
    public class AuthManager : IAuthManager
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;

        public AuthManager(ApplicationDbContext dbContext,
            IHttpContextAccessor httpContext) 
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
        }

        public async Task login(string username, string password)
        {
            var user = await _dbContext.users.FirstOrDefaultAsync(x => x.Name.ToLower() == username.ToLower().Trim());
            if (user == null)
            {
                throw new Exception("Invalid username");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new Exception("Username and password do not match");
            }

            var httpContext = _httpContext.HttpContext;
            var claims = new List<Claim>
        {
            new("Id", user.Id.ToString())
        };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }

        public async Task logout()
        {
            await _httpContext.HttpContext.SignOutAsync();
        }
    }
}
