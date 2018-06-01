using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LostInLublin.Core.ViewModels
{
    public class StartViewModel:MvxViewModel
    {
        IMvxNavigationService _navigation;
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
        public MvxCommand SearchCmd { get; private set; }

        public StartViewModel(IMvxNavigationService navigationService)
        {
            _navigation = navigationService;
            SearchCmd = new MvxCommand(() =>
            {
                _navigation.Navigate<PostsViewModel>();
            });

            EndDate = DateTime.Now;
        }
    }
}
