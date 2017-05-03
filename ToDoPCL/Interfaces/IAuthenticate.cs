using System.Threading.Tasks;

namespace ToDoPCL.Interfaces
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();

        Task<bool> Logout();
    }
}
