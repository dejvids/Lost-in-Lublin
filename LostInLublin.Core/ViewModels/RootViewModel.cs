using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LostInLublin.Core.ViewModels
{
    public class RootViewModel : MvxViewModel
    {
        IMvxNavigationService _navigationService;
        public RootViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
           // navigationService.Navigate<PostsViewModel>();
        }
        private async Task Navigate()

        {

            try

            {

                await _navigationService.Navigate<PostsViewModel>();

            }

            catch (System.Exception)

            {

            }

        }
    }
}
