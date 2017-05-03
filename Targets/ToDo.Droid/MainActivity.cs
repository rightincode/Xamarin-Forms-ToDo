using Microsoft.WindowsAzure.MobileServices;

using System;
using System.Threading.Tasks;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;

using ToDoPCL.Interfaces;

namespace ToDo.Droid
{    
	[Activity (Label = "ToDo", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, IAuthenticate
	{
        private MobileServiceUser user;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

            ToDoPCL.ToDoPCL.Init((IAuthenticate)this);

			LoadApplication (new ToDoPCL.ToDoPCL ());
		}

        public async Task<bool> Authenticate()
        {
            var success = false;
            var message = string.Empty;

            try
            {
                // Sign in with Azure Active Directory, login using a server-managed flow.
                var user = await ToDoPCL.ToDoPCL.Database.MobileService.LoginAsync(this,
                    MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);

                if (user != null)
                {
                    message = string.Format("You are signed-in as {0}.",
                        user.UserId);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            // Display the success or failure message.
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle("Sign-in result");
            builder.Create().Show();

            return success;
        }

        public async Task<bool> Logout()
        {
            bool authenticated = false;
            CookieManager.Instance.RemoveAllCookie();

            await ToDoPCL.ToDoPCL.Database.MobileService.LogoutAsync();
            return authenticated;
        }
    }    
}

