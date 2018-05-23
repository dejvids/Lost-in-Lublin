using System;
using Android.App;
using Firebase.Iid;
using Android.Util;
using WindowsAzure.Messaging;

using System.Collections.Generic;
using LostInLublin.Droid;

namespace lostinlublin.droid.services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.instance_id_event" })]
    public class myfirebaseiidservice : FirebaseInstanceIdService
    {
        const string tag = "myfirebaseiidservice";
        NotificationHub hub;

        public override void OnTokenRefresh()
        {
            var refreshedtoken = FirebaseInstanceId.Instance.Token;
            Log.Debug(tag, "fcm token: " + refreshedtoken);
            sendregistrationtoserver(refreshedtoken);
        }

        void sendregistrationtoserver(string token)
        {
            // register with notification hubs
            hub = new NotificationHub(Constants.NotificationHubName,
                                      Constants.ListenConnectionString, this);

            var tags = new List<string>() { };
            var regid = hub.Register(token, tags.ToArray()).RegistrationId;

            Log.Debug(tag, $"successful registration of id {regid}");
        }
    }
}