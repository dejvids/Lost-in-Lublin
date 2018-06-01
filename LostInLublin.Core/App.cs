using LostInLublin.Core.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Localization;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LostInLublin.Core
{
    public class App: MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            RegisterAppStart<PostsViewModel>();

        }

        /// <summary>

        /// Do any UI bound startup actions here

        /// </summary>

        /// <param name="hint"></param>

        public override void Startup(object hint)

        {

            base.Startup(hint);

        }



        /// <summary>

        /// If the application is restarted (eg primary activity on Android 

        /// can be restarted) this method will be called before Startup

        /// is called again

        /// </summary>

        public override void Reset()

        {

            base.Reset();

        }
    }
}
