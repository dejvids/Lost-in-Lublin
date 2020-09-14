using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LostInLublin.Models;
using System.Web;
using System.Net;

namespace LostInLublin.Controllers
{
    [Produces("application/json")]
    [Route("api/Notification")]
    public class NotificationController : Controller
    {
        [HttpPost]
        public async void Post(string id)
        {
            //var user = HttpContext.User.Identity.Name;
            //string[] userTag = new string[2];
            //userTag[0] = "username:" + "";
            //userTag[1] = "from:" + user;

            //Microsoft.Azure.NotificationHubs.NotificationOutcome outcome = null;
            //HttpStatusCode ret = HttpStatusCode.InternalServerError;
            //string pns = notification.Pns;
            //string message = notification.Message;


            //switch (pns.ToLower())
            //{
            //    case "wns":
            //        // Windows 8.1 / Windows Phone 8.1
            //        var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">" +
            //                    "From " + user + ": " + message + "</text></binding></visual></toast>";
            //        outcome = await Notifications.Instance.Hub.SendWindowsNativeNotificationAsync(toast, userTag);
            //        break;
            //    case "apns":
            //        // iOS
            //        var alert = "{\"aps\":{\"alert\":\"" + "From " + user + ": " + message + "\"}}";
            //        outcome = await Notifications.Instance.Hub.SendAppleNativeNotificationAsync(alert, userTag);
            //        break;
            //    case "gcm":
            //        // Android
            //        var notif = "{ \"data\" : {\"message\":\"" + message + "\"}}";
            //        outcome = await Notifications.Instance.Hub.SendGcmNativeNotificationAsync(notif);

            //        break;
            //}

            //if (outcome != null)
            //{
            //    if (!((outcome.State == Microsoft.Azure.NotificationHubs.NotificationOutcomeState.Abandoned) ||
            //        (outcome.State == Microsoft.Azure.NotificationHubs.NotificationOutcomeState.Unknown)))
            //    {
            //        return Ok();
            //    }
            //}

            //return StatusCode(500);
            var registrations = Notifications.Instance.Hub.GetAllRegistrationsAsync(10);
            var registration = registrations.Result.FirstOrDefault();
            if (registration != null)
                await Notifications.Instance.Hub.DeleteRegistrationAsync(id);
        }


        [Route("api/notification/cancel")]
        [HttpPost]
        public async void Cancel()
        {


        }
    }
}