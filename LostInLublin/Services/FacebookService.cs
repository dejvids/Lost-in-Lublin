using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LostInLublin.Services
{
    public interface IFacebookService
    {
        Task<Account> GetAccountAsync(string accessToken);
        Task<string> PostOnWallAsync(string accessToken, string message);
        Task<IEnumerable<PostDto>> GetPostsAsync(string accessToken, string pageId, DateTime createdDate,DateTime minDate);
    }

    public class FacebookService : IFacebookService
    {
        private readonly IFacebookClient _facebookClient;

        public FacebookService(IFacebookClient facebookClient)
        {
            _facebookClient = facebookClient;
        }

        public async Task<Account> GetAccountAsync(string accessToken)
        {
            var result = await _facebookClient.GetAsync<dynamic>(
                accessToken, "me", "fields=id,name,email,first_name,last_name,age_range,birthday,gender,locale");

            if (result == null)
            {
                return new Account();
            }

            var account = new Account
            {
                Id = result.id,
                Email = result.email,
                Name = result.name,
                UserName = result.username,
                FirstName = result.first_name,
                LastName = result.last_name,
                Locale = result.locale
            };

            return account;
        }

        public async Task<IEnumerable<PostDto>> GetPostsAsync(string accessToken, string pageId, DateTime createdDate, DateTime minDate)
        {
            var result = await _facebookClient.GetAsync<Posts>(
                accessToken, pageId,$"fields=full_picture,created_time,message&since={createdDate.ToShortDateString()}&until={minDate.ToShortDateString()}&limit=100");
            if (result == null)
                return new List<PostDto>();
            var posts = result.Data;
            return posts;
        }

        public async Task<string> PostOnWallAsync(string accessToken, string message)
            => await _facebookClient.PostAsync(accessToken, "/feed", new {message});
    }  
}