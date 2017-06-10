using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using System.Collections.Generic;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.Mobile.Server.Config;
using Sharit.Azure.Backend.DataObjects;
using Sharit.Azure.Backend.Models;

namespace Xamagram.Controllers
{
    public class SharitItemController : TableController<SharitItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<SharitItem>(context, Request);
        }

        // GET tables/XamagramItem
        public IQueryable<SharitItem> GetAllXamagramItem()
        {
            return Query();
        }

        // GET tables/XamagramItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<SharitItem> GetXamagramItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/XamagramItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<SharitItem> PatchXamagramItem(string id, Delta<SharitItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/XamagramItem
        public async Task<IHttpActionResult> PostXamagramItem(SharitItem item)
        {
            SharitItem current = await InsertAsync(item);

            // Obtenemos la configuración para el proyecto server.
            HttpConfiguration config = this.Configuration;
            MobileAppSettingsDictionary settings =
                Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();

            // Obtenemos las credenciales del Notification Hubs para la Mobile App.
            string notificationHubName = settings.NotificationHubName;
            string notificationHubConnection = settings
                .Connections[MobileAppSettingsKeys.NotificationHubConnectionString].ConnectionString;

            // Creamos el cliente Notification Hub.
            NotificationHubClient hub = NotificationHubClient
            .CreateClientFromConnectionString(notificationHubConnection, notificationHubName);

            // APNS, GCM, WNS, and MPNS templates.
            Dictionary<string, string> templateParams = new Dictionary<string, string>();
            templateParams["messageParam"] = item.Title + " ha sido añadido!.";

            try
            {
                // Enviar la notificación push.
                var result = await hub.SendTemplateNotificationAsync(templateParams);

                // Escribimos el resultado en los logs.
                config.Services.GetTraceWriter().Info(result.State.ToString());
            }
            catch (System.Exception ex)
            {
                // Escribimos el resultado del fallo en los logs.
                config.Services.GetTraceWriter()
                    .Error(ex.Message, null, "Push.SendAsync Error");
            }

            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/XamagramItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteXamagramItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}