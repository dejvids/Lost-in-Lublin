using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Plugin.Location;
using MvvmCross;

namespace LostInLublin.Core.ViewModels
{
    public class StartViewModel:MvxViewModel
    {
        IMvxNavigationService _navigation;
        IMvxLocationWatcher _watcher;
        DateTime startDate;
        DateTime endDate;
        string location;
        string keyword;
        public string Location { get; set; }
        public string KeyWord { get; set; }
        public DateTime StartDate
        {
            get { return startDate; }
            set { this.RaiseAndSetIfChanged(ref startDate, value); }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set { this.RaiseAndSetIfChanged(ref endDate, value); }
        }

        private double lang;

        public double Long
        {
            get { return lang; }
            set { this.RaiseAndSetIfChanged(ref lang, value); }
        }

        private double lat;

        public double Lat
        {
            get { return lat; }
            set { this.RaiseAndSetIfChanged(ref lat, value); }
        }


        public MvxCommand SearchCmd { get; private set; }

        public StartViewModel(IMvxNavigationService navigationService, IMvxLocationWatcher watcher)
        {
            _navigation = navigationService;
            _watcher = watcher;
            _watcher.Start(new MvxLocationOptions(), OnLocation, OnError);
            SearchCmd = new MvxCommand(() =>
            {
                _navigation.Navigate<PostsViewModel>();
            });

            EndDate = DateTime.Now;
        }

        private void OnLocation(MvxGeoLocation obj)
        {
            Lat = obj.Coordinates.Latitude;
            Long = obj.Coordinates.Longitude;
        }

        private void OnError(MvxLocationError obj)
        {
          // Mvx.
        }
    }
}
