using MvvmCross.Commands;
using MvvmCross.Plugin.WebBrowser;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LostInLublin.Core.ViewModels
{
    public class DetailViewModel:MvxViewModel<Post>
    {
        private string message;
        private string picture;
        private IMvxWebBrowserTask _webBrowser;

        public string Message { get { return message; } set { this.RaiseAndSetIfChanged(ref message, value); } }
        public string Picture
        {
            get { return picture; }
            set { this.RaiseAndSetIfChanged(ref picture, value); }
        }

        public string URL { get; set; }

        public MvxCommand OpenLinkCmd { get; private set; }
        public string Created { get; set; }

        public DetailViewModel(IMvxWebBrowserTask webBrowser)
        {
            this._webBrowser = webBrowser;
            OpenLinkCmd = new MvxCommand(() =>
            {
                _webBrowser.ShowWebPage(URL);
            });
        }

        public override void Prepare()
        {
            base.Prepare();
        }

        public override void Prepare(Post parameter)
        {
            Message = parameter.Message;
            Picture = parameter.Picture;
            URL = parameter.URL;
            Created = parameter.CreatedDate.ToLongDateString();
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }
       
    }
}
