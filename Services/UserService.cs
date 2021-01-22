using client_server.Data.Models;
using client_server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Schools.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Schools.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext data;
        private readonly UserManager<UsersModel> userManager;

        public UserService(ApplicationDBContext data,
                           UserManager<UsersModel> userManager)
        {
            this.data = data;
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
