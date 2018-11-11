using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;
using Xamarin.Forms;
using ToDo.UWP;
using ToDo.Interfaces;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;

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
                var user = await ToDo.Database.MobileService.LoginAsync(
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

        public async Task<bool> AuthenticateAsync()
        {
            Authenticated = false;

            string authority = "https://login.microsoftonline.com/nbknxf2hotmail.onmicrosoft.com";
            string resourceId = "e09eb4fb-7218-4d87-af41-68d754f8d2c1";
            string clientId = "a960b30b-726e-4619-94d4-8b8687fb0414";
            string redirectUri = "https://xformstodo.azurewebsites.net/.auth/login/done";
            try
            {
                AuthenticationContext ac = new AuthenticationContext(authority);
                AuthenticationResult ar = await ac.AcquireTokenAsync(resourceId, clientId,
                    new Uri(redirectUri), new PlatformParameters(PromptBehavior.Auto, false));
                JObject payload = new JObject
                {
                    ["access_token"] = ar.AccessToken
                };
                var user = await ToDo.Database.MobileService.LoginAsync(
                    MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory, payload);

                if (user != null)
                {
                    Authenticated = true;
                }
            }
            catch (InvalidOperationException)
            {
                var message = "You must log in. Login Required";
                var dialog = new MessageDialog(message);
                dialog.Commands.Add(new UICommand("OK"));
                await dialog.ShowAsync();
            }

            return Authenticated;
        }

        public async Task<bool> LogoutAsync()
        {
            Authenticated = false;

            await ToDo.Database.MobileService.LogoutAsync();
            return Authenticated;
        }
    }    
}
