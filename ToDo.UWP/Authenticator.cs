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
    public class Authenticator : IAuthenticate
    {
        public void SetClient(object client)
        {
            throw new NotImplementedException("Method SetClient not implemented!");
        }

        public async Task<bool> Authenticate()
        {
            string message = string.Empty;
            var success = false;

            try
            {
                // Sign in with Azure Active Directory, login using a server-managed flow.
                var user = await ToDoPCL.ToDoPCL.Database.MobileService.LoginAsync(
                    MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);

                if (user != null)
                {
                    message = string.Format("You are signed-in as {0}.", user.UserId);
                    success = true;
                }

            }
            catch (Exception ex)
            {
                message = string.Format("Authentication Failed: {0}", ex.Message);
            }

            // Display the success or failure message.
            await new MessageDialog(message, "Sign-in result").ShowAsync();

            return success;
        }

        public async Task<bool> Logout()
        {
            bool authenticated = false;

            await ToDoPCL.ToDoPCL.Database.MobileService.LogoutAsync();
            return authenticated;
        }
    }    
}
