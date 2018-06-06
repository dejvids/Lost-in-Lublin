using LostInLublin.Core.Models;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.PictureChooser;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using Piller.Services;
using Services;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LostInLublin.Core.ViewModels
{
    public class AddViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;
        IMvxPictureChooserTask PictureChooser = Mvx.Resolve<IMvxPictureChooserTask>();
        //  private IPermanentStorageService storage = Mvx.Resolve<IPermanentStorageService>();
        // private readonly ImageLoaderService imageLoader = Mvx.Resolve<ImageLoaderService>();
        private string endpoint = Settings.PostsEndpoint;
        private string message;

        public string Message
        {
            get { return message; }
            set { this.RaiseAndSetIfChanged(ref message, value); }
        }

        private DateTime createdDate;

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { this.RaiseAndSetIfChanged(ref createdDate, value); }
        }

        private byte[] picture;

        public byte[] Picture
        {
            get { return picture; }
            set { this.RaiseAndSetIfChanged(ref picture, value); }
        }

        public MvxCommand AddCmd { get; private set; }
        public MvxCommand TakePhotoCommand { get; private set; }

        public AddViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            AddCmd = new MvxCommand(async () =>
            {
                PostDto post = new PostDto { Message = this.Message, Full_Picture = Convert.ToBase64String(Picture), Created_Time = this.CreatedDate.ToLongDateString() };
                //if (this.Picture != null)
                //{
                //    imageLoader.SaveImage(this.Picture, $"lost_{message}");
                //}
                var jsonObject = JsonConvert.SerializeObject(post);
                HttpClient client = new HttpClient();

                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                await client.PostAsync(endpoint, content);
                await _navigationService.Navigate<PostsViewModel>();

            }, () => !string.IsNullOrEmpty(Message));

            TakePhotoCommand = new MvxCommand(async () =>
            {
                var stream = await PictureChooser.TakePicture(1920, 75);
                if(stream !=null )
                    await OnPicture(stream);
                });
        }

        private async Task OnPicture(Stream pictureStream)
        {

            var memoryStream = new MemoryStream();
            await pictureStream.CopyToAsync(memoryStream);
            Picture = memoryStream.ToArray();
        }
    }
}