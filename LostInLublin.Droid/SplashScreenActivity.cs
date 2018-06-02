using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Views;

namespace LostInLublin.Droid
{
    [Activity(
          Label = "Zgubione w Lublinie"
          , MainLauncher = true
          , Icon = "@drawable/icon64x64"
          , Theme = "@style/Theme.Splash"
          , NoHistory = true
          , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}