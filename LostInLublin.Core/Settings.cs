using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostInLublin.Core
{
    public static class Settings
    {
        private static string baseEndpoint = "https://zgubionewlublinie.azurewebsites.net";
       // private static string baseEndpoint = "http://10.0.2.2:50920";
        public static string PostsEndpoint { get; } = $"{baseEndpoint}/api/posts";

        public static string CancelNotificationsEndpoint { get; } = $"{baseEndpoint}/api/noification";
    }
}