using Xamarin.Forms;

namespace ToDoPCL
{
    public class ToDoPCL : Application
    {
        public ToDoPCL()
        {
            MainPage = new NavigationPage(new CreatePage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
