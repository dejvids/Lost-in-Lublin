using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LostInLublin.Droid
{
    public static class Constants
    {
        public const string ListenConnectionString = "Endpoint=sb://notificationprojnamespace.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=UcluffX9GK8XBhNsGXnS0b1nHK6SJGzXCnvZnqM8Jc8=";
        public const string NotificationHubName = "notificationProj";
    }
}