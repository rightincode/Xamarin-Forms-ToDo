using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using System.Collections.Generic;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server;
using ToDo.MobileAppService.DataObjects;
using ToDo.MobileAppService.Models;

namespace ToDo.MobileAppService.Controllers
{
    [Authorize]
    public class TodoItemController : TableController<ToDoItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<ToDoItem>(context, Request, true);
        }

        // GET tables/TodoItem
        public IQueryable<ToDoItem> GetAllTodoItems()
        {
            return Query();
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ToDoItem> GetTodoItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<ToDoItem> PatchTodoItem(string id, Delta<ToDoItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostTodoItem(ToDoItem item)
        {
            ToDoItem current = await InsertAsync(item);

            if (current != null)
            {
                await SentPushNotificationAsync(item);
            }

            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }

        private async Task SentPushNotificationAsync(ToDoItem item)
        {
            HttpConfiguration config = this.Configuration;
            MobileAppSettingsDictionary settings =
                this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();

            string notificationHubName = settings.NotificationHubName;
            string notificationHubConnection = settings
                .Connections[MobileAppSettingsKeys.NotificationHubConnectionString].ConnectionString;

            NotificationHubClient hub = 
                NotificationHubClient.CreateClientFromConnectionString(notificationHubConnection, notificationHubName);

            Dictionary<string, string> templateParams = new Dictionary<string, string>
            {
                ["messageParam"] = item.TaskName + " was added to the list."
            };

            try
            {
                // Send the push notification and log the results.
                var result = await hub.SendTemplateNotificationAsync(templateParams);

                // Write the success result to the logs.
                config.Services.GetTraceWriter().Info(result.State.ToString());
            }
            catch (System.Exception ex)
            {
                // Write the failure result to the logs.
                config.Services.GetTraceWriter()
                    .Error(ex.Message, null, "Push.SendAsync Error");
            }
        }
    }
}