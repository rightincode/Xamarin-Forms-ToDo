using Xamarin.Forms;

using ToDoPCL.Data;
using ToDoPCL.Interfaces;

namespace ToDoPCL
{
    public class ToDoPCL : Application
    {
        static ToDoItemDatabase database;

        public static ToDoItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ToDoItemDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLiteSync2.db3"));
                }

                return database;
            }
        }

        static object currentAppContext;
        static IAuthenticate authenticator;

        public static IAuthenticate Authenticator {
            get {

                if (authenticator == null)
                {
                    authenticator = DependencyService.Get<IAuthenticate>();

                    if (Device.RuntimePlatform != Device.UWP)
                    {
                        authenticator.SetClient(currentAppContext);
                    } else
                    {
                        authenticator.SetClient(Database.MobileService);
                    }                   
                }

                return authenticator;
            }
        }

        public static void Init(object CurrentAppContext)
        {
            currentAppContext = CurrentAppContext;
        }

        public ToDoPCL()
        {
            MainPage = new NavigationPage(new ListTasksPage());
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
