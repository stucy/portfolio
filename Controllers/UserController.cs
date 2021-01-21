using client_server.Data.Models;
using client_server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Schools.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<UsersModel> userManager;
        private readonly SignInManager<UsersModel> signInManager;
        /*private readonly IUserService userService;*/

        public UserController(UserManager<UsersModel> userManager,
                                 SignInManager<UsersModel> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            /*this.userService = userService;*/
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(nameof(Register));
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            /*await this.userService.Create(model);*/

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(nameof(Login));
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await this.userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return View();
            }

            var logInResult = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!logInResult.Succeeded)
            {
                return View(nameof(Login), model);
            }

            return RedirectToAction("Index", "Student");
        }
    }
}
