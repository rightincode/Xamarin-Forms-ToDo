﻿using Xamarin.Forms;

using ToDoPCL.Data;

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
