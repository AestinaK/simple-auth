using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simple_authentication.Constants;
using simple_authentication.Data;
using simple_authentication.Entity;
using System.Transactions;

namespace simple_authentication.Controllers
{
    [AllowAnonymous]
    public class SeedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeedController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> SeedSuperAdmin()
        {

            try
            {
                var previousSuperAdminExists = await _context.users.AnyAsync(x => x.Type == UserType.admin);
                if (!previousSuperAdminExists)
                {
                    using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                    var admin = new User()
                    {
                        Email = "super.admin",
                        Type = UserType.admin,
                        Name = "Super Admin",
                        Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                        CreatedDate = DateTime.UtcNow,
                        Address = "Btm",
                        Phone = "9800000000",
                        Gender = "Email"
                    };
                    _context.users.Add(admin);
                    await _context.SaveChangesAsync();
                    tx.Complete();
                    return Content("User Seeding Complete");
                }

                return Content("User already seeded");
            }
            catch (Exception e)
            {
                return Content(e.Message);
               
            }
          
        }
    }
}
