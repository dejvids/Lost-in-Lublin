using System;
using Android.App;
using Firebase.Iid;
using Android.Util;
using WindowsAzure.Messaging;

using System.Collections.Generic;
using LostInLublin.Droid;
using Android.Content;
using Android.Preferences;

namespace LostInLublin.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        public static string RegistrationId;
        const string TAG = "MyFirebaseIIDService";
        NotificationHub hub;

        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "FCM token: " + refreshedToken);
            SendRegistrationToServer(refreshedToken);
        }

        void SendRegistrationToServer(string token)
        {
            // Register with Notification Hubs
            hub = new NotificationHub(Constants.NotificationHubName,
                                      Constants.ListenConnectionString, this);

            var tags = new List<string>() {"p" };
            var regID = hub.Register(token, tags.ToArray()).RegistrationId;
            RegistrationId = regID;


            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();

            editor.PutString("reg_id", regID);
            editor.Apply();
            Log.Debug(TAG, $"Successful registration of ID {regID}");
        }

        public void Unregister()
        {
            hub.Unregister();
        }
    }

}