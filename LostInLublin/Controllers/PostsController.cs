using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LostInLublin.Models;
using LostInLublin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;

using System.Diagnostics;

namespace LostInLublin.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly PostsDbContext _dbContext;
        private IConfiguration _configuration;
        private FacebookService _facebookService;
        private IEnumerable<Models.Post> _posts;
        private DateTime Lastdate;
        public PostsController(PostsDbContext dbContext, IConfiguration configuration)
        {
            this._dbContext = dbContext;
            _configuration = configuration;
            _facebookService = new FacebookService(new FacebookClient());
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                await GetPostsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            var posts = await _dbContext.Posts.ToListAsync();
            Lastdate = posts.Max(p => p.CreatedDate);

            this._posts = posts;
            return Ok(posts.OrderByDescending(x => x.CreatedDate));
           // return Ok(posts);
        }

       
        public async  Task<IEnumerable<PostDto>> GetPostsAsync()
        {
            var createdDate =  _dbContext.Posts.Max(p => p.CreatedDate);
            var minDate = DateTime.Now;
            IEnumerable<PostDto> posts = new List<Services.PostDto>();
            var accountTask = _facebookService.GetAccountAsync(FacebookSettings.AccessToken);
            Task.WaitAll(accountTask);
            var account = accountTask.Result;
            while (minDate >= createdDate)
            {
                foreach(var endpoint in FacebookSettings.Endpoints)
                {
                    posts = posts.Concat(await _facebookService.GetPostsAsync(_configuration.GetSection("AccessToken").GetValue<string>("UserTOken"), endpoint.Id, createdDate, minDate.AddDays(1)));
                }
                //var postsTask = facebookService.GetPostsAsync(FacebookSettings.AccessToken, FacebookSettings.SpottedLublin, createdDate, minDate);
                //Task.WaitAll(postsTask);
                //posts = posts.Concat(postsTask.Result);
                //var pollubPosts = facebookService.GetPostsAsync(FacebookSettings.AccessToken, FacebookSettings.SpottetPollub, createdDate, minDate).Result;
                //posts = posts.Concat(facebookService.GetPostsAsync(FacebookSettings.AccessToken, FacebookSettings.SpottedMpk, createdDate, minDate).Result);
                //posts = posts.Concat(facebookService.GetPostsAsync(FacebookSettings.AccessToken, FacebookSettings.SpottedLublin2, createdDate, minDate).Result);
                //posts = posts.Concat(pollubPosts);
                if (posts.FirstOrDefault(p => p.CreatedTimeDate == posts.Min(x => x.CreatedTimeDate)).CreatedTimeDate == minDate)
                    break;
                minDate = posts.FirstOrDefault(p => p.CreatedTimeDate == posts.Min(x => x.CreatedTimeDate)).CreatedTimeDate;
            }
            var newPosts = posts.Where(p => FacebookSettings.KeyWords.Any(w => p.Message != null && p.Message.ToLower().Contains(w)) && !_dbContext.Posts.ToList().Any(x => x.Id == p.Id));
            foreach (var post in newPosts)
            {
                _dbContext.Posts.Add(new Models.Post()
                {
                    Id = post.Id,
                    Message = post.Message,
                    CreatedDate = post.CreatedTimeDate,
                    Picture = post.Full_Picture,
                    URL = post.Url
                });
                _dbContext.SaveChanges();
            }
            return newPosts;
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]PostDto post)
        {
            post.Id = PublishPost(post);
            _dbContext.Posts.Add(new Post()
            {
                Id = post.Id,
                Message = post.Message,
                Picture = post.Full_Picture,
                CreatedDate = DateTime.Now,
                URL = post.Url
            });
            _dbContext.SaveChanges();
        }

        private string PublishPost(PostDto post)
        {
            var postTask = _facebookService.PostOnWallAsync(_configuration.GetSection("AccessToken").GetValue<string>("UserToken"), post.Message);
            Task.WaitAll(postTask);
            return postTask.Result;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
