using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
//using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
//using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LostInLublin.Core.ViewModels
{
    public class PostsViewModel : MvxViewModel
    {
        IMvxNavigationService _navigationService;
        private ObservableCollection<Post> posts;
        private MvxCommand<Post> goDetailsCmd;
        //private string endpoint = @"https://zgubionewlublinie.azurewebsites.net/api/posts";

        private string endpoint = @"http://10.0.2.2:50920/api/posts";
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
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync(endpoint);
            var content = result.Content.ReadAsStringAsync().Result;
            Posts = new MvxObservableCollection<Post>(JsonConvert.DeserializeObject<IEnumerable<Post>>(content));
            return JsonConvert.DeserializeObject<IEnumerable<Post>>(content);
        }
    }
}
