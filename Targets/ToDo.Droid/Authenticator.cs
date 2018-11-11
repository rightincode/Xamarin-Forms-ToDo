using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;

using Xamarin.Forms;

using ToDo.Droid;
using ToDo.Interfaces;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;

[assembly: Dependency(typeof(Authenticator))]
namespace ToDo.Droid
{
    public class Authenticator : IAuthenticator
    {
        private Context currentClient;

        public bool Authenticated { get; set; } = false;

        public object GetClient()
        {
            return currentClient;
        }

        public void SetClient(object client)
        {
            currentClient = (Context)client;
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
                    new Uri(redirectUri), new PlatformParameters((Activity)currentClient));
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
            catch (Exception ex)
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(currentClient);
                builder.SetMessage(ex.Message);
                builder.SetTitle("You must log in. Login Required");
                builder.Create().Show();
            }
            
            return Authenticated;
        }

        public async Task<bool> LogoutAsync()
        {
            Authenticated = false;
            CookieManager.Instance.RemoveAllCookie();

            await ToDo.Database.MobileService.LogoutAsync();
            return Authenticated;
        }
    }
}