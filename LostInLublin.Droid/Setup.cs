using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LostInLublin.Core;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Logging;
using MvvmCross.Platforms.Android.Presenters;
using Services;

namespace LostInLublin.Droid
{

        
        public class Setup : MvxAppCompatSetup<App>

        {

            //protected override IEnumerable<Assembly> AndroidViewAssemblies =>

            //    new List<Assembly>(base.AndroidViewAssemblies)

            //    {

            //    typeof(MvxRecyclerView).Assembly

            //    };



            public override MvxLogProviderType GetDefaultLogProviderType()

                => MvxLogProviderType.Serilog;

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pl-PL");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pl-PL");

            Mvx.RegisterSingleton<ImageLoaderService>(new AndroidImageLoader());
            return base.CreateViewPresenter();
        }

        //protected override IMvxLogProvider CreateLogProvider()

        //{

        //    Log.Logger = new LoggerConfiguration()

        //        .MinimumLevel.Debug()

        //        .WriteTo.AndroidLog()

        //        .CreateLogger();

        //    return base.CreateLogProvider();

        //}

    }

    
        
    }
    
