using System.Threading.Tasks;


namespace ToDoPCL.Interfaces
{
    public interface IAuthenticate
    {
        void SetClient(object currentClient);

        Task<bool> Authenticate();

        Task<bool> Logout();
    }
}
