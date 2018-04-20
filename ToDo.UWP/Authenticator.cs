using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;
using Xamarin.Forms;
using ToDo.UWP;
using ToDoPCL.Interfaces;

[assembly: Dependency(typeof(Authenticator))]
namespace ToDo.UWP
{
    public class Authenticator : IAuthenticator
    {
        private MobileServiceClient _currentMobileClient;

        public bool Authenticated { get; set; } = false;

        public object GetClient()
        {
            return _currentMobileClient;
        }

        public void SetClient(object client)
        {
            _currentMobileClient = (MobileServiceClient)client;            
        }

        public async Task<bool> Authenticate()
        {
            string message = string.Empty;
            Authenticated = false;

            try
            {
                // Sign in with Azure Active Directory, login using a server-managed flow.
                var user = await ToDoPCL.ToDoPCL.Database.MobileService.LoginAsync(
                    MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory, "xformstodo");

                if (user != null)
                {
                    message = string.Format("You are signed-in as {0}.", user.UserId);
                    Authenticated = true;
                }

            }
            catch (Exception ex)
            {
                message = string.Format("Authentication Failed: {0}", ex.Message);
            }

            // Display the success or failure message.
            await new MessageDialog(message, "Sign-in result").ShowAsync();

            return Authenticated;
        }

        public async Task<bool> Logout()
        {
            Authenticated = false;

            await ToDoPCL.ToDoPCL.Database.MobileService.LogoutAsync();
            return Authenticated;
        }
    }    
}
