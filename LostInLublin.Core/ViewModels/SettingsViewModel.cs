using Cheesebaron.MvxPlugins.Settings.Interfaces;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;

namespace LostInLublin.Core.ViewModels
{
    public class SettingsViewModel :MvxViewModel
    {
        private IMvxNavigationService _navigation;

        private bool notificationOn;
        //private ISettings settings = Mvx.Resolve<ISettings>();
        private const string settingKey = "notification_on";

        public bool NotificationOn
        {
            get { return notificationOn; }
            set { notificationOn = value; }
        }


        public MvxCommand<string>  SaveCmd { get; private set; }

        public SettingsViewModel(IMvxNavigationService navigation)
        {
            _navigation = navigation;
           // NotificationOn = settings.GetValue<bool>(settingKey);

            SaveCmd = new MvxCommand<string>(async id =>
            {
                if (NotificationOn == false)
                {
                    CancelNotificationsAsync(id);
                }
                await _navigation.Navigate<PostsViewModel>();
               // settings.AddOrUpdateValue<bool>(settingKey, NotificationOn);
            });


        }

        private async void CancelNotificationsAsync(string id)
        {
            HttpClient client = new HttpClient();
            HttpContent content = new StringContent(id);

            await client.PostAsync(Settings.CancelNotificationsEndpoint, content);
        }
    }
}
