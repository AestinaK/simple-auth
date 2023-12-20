using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simple_authentication.Data;
using simple_authentication.Manager.Interface;
using simple_authentication.ViewModel;

namespace simple_authentication.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthManager _authManager;

        public AuthController(ApplicationDbContext context,
            IAuthManager authManager)
        {
           _context = context;
           _authManager = authManager;
        }
        public IActionResult login()
        {
            var vm = new LoginVm();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> login(LoginVm vm) {
        
        if(!ModelState.IsValid)
            {
                return View(vm);
            }

            await _authManager.login(vm.UserName, vm.Password);
            return RedirectToAction("Index","Home");
        
        }
        public async Task<IActionResult> logout()
        {
            await _authManager.logout();
            return RedirectToAction(nameof(login));
        }
    }
}
