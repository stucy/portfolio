using client_server.Models;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
    public interface IUserService
    {
        Task Create(RegisterModel model);

    }
}