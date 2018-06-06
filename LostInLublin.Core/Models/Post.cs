using System;

namespace LostInLublin.Core.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedDate { get; set; }
        public string URL { get; set; }
    }
}