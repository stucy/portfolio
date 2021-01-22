using client_server.Models;
using System.Threading.Tasks;

namespace client_server.Services.Interfaces
{
    public interface IUserService
    {
        Task Create(RegisterModel model);

    }
}