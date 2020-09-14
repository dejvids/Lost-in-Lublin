using LostInLublin.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
//using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
//using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LostInLublin.Core.ViewModels
{
    public class PostsViewModel : MvxViewModel<SearchModel>
    {
        IMvxNavigationService _navigationService;
        
        private ObservableCollection<Post> posts;
        private MvxCommand<Post> goDetailsCmd;
        //private string endpoint = @"https://zgubionewlublinie.azurewebsites.net/api/posts";

        private string endpoint = Settings.PostsEndpoint;
        public ObservableCollection<Post> Posts { get { return posts; } set { SetProperty(ref posts, value); } }
        public ICommand GoDetailsCmd
        {
            get
            {
                return goDetailsCmd =  goDetailsCmd ?? new MvxCommand<Post>(async post =>
                {
                    var b = _navigationService.CanNavigate<DetailViewModel>();
                    await _navigationService.Navigate<DetailViewModel,Post>(post);
                });
            }
        }
        public SearchModel SearchObject { get; private set; }
        private bool progressBarVisible;
        public bool ProgressBarVisibile
        {
            get { return progressBarVisible; }
            set { this.RaiseAndSetIfChanged(ref progressBarVisible, value); }
        }

        public MvxCommand AddItemCmd { get; private set; }
        public MvxCommand SettingsCmd { get; private set; }

        public PostsViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            //    Posts = new MvxObservableCollection<Post>
            //{
            //    new Post(){Id = "1", Message="Znaleziono dom", CreatedDate= DateTime.Now, Picture=@"https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/Macaca_nigra_self-portrait_large.jpg/800px-Macaca_nigra_self-portrait_large.jpg"},
            //    new Post(){Id = "2", Message="Znaleziono nóż", CreatedDate= DateTime.Now, Picture=@"https://scontent.xx.fbcdn.net/v/t1.0-0/p180x540/31958914_1891411460903807_2256863627371872256_n.jpg?_nc_cat=0&oh=78504e0c60617d32263d0c4d8becdeb8&oe=5B8C289E" },
            //    new Post(){Id = "2", Message="Znaleziona hehe", CreatedDate= DateTime.Now, Picture="@drawable/place.png" }

            //    };
            //GetPostsCmd = ReactiveCommand.Create(() =>
            //{
            //    Posts = new MvxObservableCollection<Post>(GetPosts().Result);
            //});
            AddItemCmd = new MvxCommand(() =>
            {
                _navigationService.Navigate<AddViewModel>();
            });

            SettingsCmd = new MvxCommand(() =>
            {
                _navigationService.Navigate<SettingsViewModel>();
            });
        }

        public async void GetPosts()
        {
            ProgressBarVisibile = true;
            HttpClient client = new HttpClient();
            var resultString = await client.GetAsync(endpoint);
            var content = resultString.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IEnumerable<Post>>(content);
            if (!string.IsNullOrEmpty(SearchObject.KeyWord))
                result = result.Where(x => x.Message.Contains(SearchObject.KeyWord));
            if (!string.IsNullOrEmpty(SearchObject.StartDate))
                result = result.Where(x => x.CreatedDate > DateTime.Parse(SearchObject.StartDate));
            if (!string.IsNullOrEmpty(SearchObject.EndDate))
                result = result.Where(x => x.CreatedDate < DateTime.Parse(SearchObject.EndDate));
            Posts = new ObservableCollection<Post>(result);
            ProgressBarVisibile = false;
        }

        public override void Prepare(SearchModel parameter)
        {
            this.SearchObject = parameter ?? new SearchModel();
        }
    }
}
