using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Plugin.Location;
using MvvmCross;
using LostInLublin.Core.Models;

namespace LostInLublin.Core.ViewModels
{
    public class StartViewModel : MvxViewModel
    {
        IMvxNavigationService _navigation;
        IMvxLocationWatcher _watcher;
        string startDate;
        string endDate;
        string location = "Lublin";
        string keyword;
        public string Location
        {
            get { return location; }
            set { this.RaiseAndSetIfChanged(ref location, value); }
        }
        public string KeyWord { get; set; }
        public string StartDate
        {
            get { return startDate; }
            set { this.RaiseAndSetIfChanged(ref startDate, value); }
        }
        public string EndDate
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
        public MvxCommand SetLocationCmd { get; private set; }

        public StartViewModel(IMvxNavigationService navigationService, IMvxLocationWatcher watcher)
        {
            _navigation = navigationService;
            _watcher = watcher;
            _watcher.Start(new MvxLocationOptions(), OnLocation, OnError);
            SearchCmd = new MvxCommand(() =>
            {
                _navigation.Navigate<PostsViewModel,SearchModel>(new SearchModel { KeyWord = this.KeyWord, StartDate = this.StartDate, EndDate = this.EndDate});
            });

            EndDate = DateTime.Now.ToShortDateString();

            SetLocationCmd = new MvxCommand(() =>
            {
                // Long = _watcher.CurrentLocation.Coordinates.Longitude;
                // Lat = _watcher.CurrentLocation.Coordinates.Latitude;
                Location = "Lublin";
            });
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
