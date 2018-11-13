using Xamarin.Forms;

using ToDo.Data;
using ToDo.Interfaces;

namespace ToDo
{
    public class ToDo : Application
    {
        static ToDoItemDatabase database;

        public static ToDoItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new 
                        
                        ToDoItemDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLiteSync2.db3"));
                }

                return database;
            }
        }

        static object currentAppContext;
        static IAuthenticator authenticator;

        public static IAuthenticator Authenticator {
            get {

                if (authenticator == null)
                {
                    authenticator = DependencyService.Get<IAuthenticator>();

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

        public ToDo()
        {
            MainPage = new NavigationPage(new ListTasksPage(Authenticator, Database));
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
