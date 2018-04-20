﻿using System;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.MobileServices;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;

using Xamarin.Forms;

using ToDo.Droid;
using ToDoPCL.Interfaces;

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

        public async Task<bool> Authenticate()
        {
            Authenticated = false;
            var message = string.Empty;

            try
            {
                // Sign in with Azure Active Directory, login using a server-managed flow.
                var user = await ToDoPCL.ToDoPCL.Database.MobileService.LoginAsync(currentClient, 
                    MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory, "xformstodo");

                if (user != null)
                {
                    message = string.Format("You are signed-in as {0}.",
                        user.UserId);
                    Authenticated = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            // Display the success or failure message.
            AlertDialog.Builder builder = new AlertDialog.Builder(currentClient);
            builder.SetMessage(message);
            builder.SetTitle("Sign-in result");
            builder.Create().Show();

            return Authenticated;
        }

        public async Task<bool> Logout()
        {
            Authenticated = false;
            CookieManager.Instance.RemoveAllCookie();

            await ToDoPCL.ToDoPCL.Database.MobileService.LogoutAsync();
            return Authenticated;
        }
    }
}