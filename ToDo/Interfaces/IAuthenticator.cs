using System.Threading.Tasks;

namespace ToDo.Interfaces
{
    public interface IAuthenticator
    {
        bool Authenticated { get; set; }

        object GetClient();

        void SetClient(object currentClient);

        Task<bool> Authenticate();

        Task<bool> Logout();
    }
}
