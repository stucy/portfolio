using client_server.Data.Models;
using client_server.Models;
using client_server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace client_server.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UsersModel> userManager;

        public UserService(UserManager<UsersModel> userManager)
        {
            this.userManager = userManager;
        }

        public async Task Create(RegisterModel model)
        {
            var user = new UsersModel
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Unnable to create user!");
            }
        }

    }
}
