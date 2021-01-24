﻿using client_server.Data.Models;
using client_server.Models;
using client_server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace client_server.Controllers
{
  public class UserController : Controller
  {
    private readonly UserManager<UsersModel> userManager;
    private readonly SignInManager<UsersModel> signInManager;
    private readonly IUserService userService;

    public UserController(UserManager<UsersModel> userManager,
                             SignInManager<UsersModel> signInManager, IUserService userService)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
      this.userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {

      if (!ModelState.IsValid)
      {
        return Json(new { communicationCode = 0, model = model });
      }

      var user = await this.userManager.FindByEmailAsync(model.Email);
      if (user != null)
      {
        return Json(new { communicationCode = 2, model = model });
      }

      await this.userService.Create(model);

      return Json(new { communicationCode = 1, model = model });
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
        return Json(new { communicationCode = 0, model = model });
      }

      var logInResult = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);
      if (!logInResult.Succeeded)
      {
        return Json(new { communicationCode = 1, model = model });
      }

      return Json(new { communicationCode = 2, model = model });
    }

    public async Task<IActionResult> Logout()
    {
      await this.signInManager.SignOutAsync();

      return RedirectToAction("Index", "Comments");
    }

  }
}
