using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostInLublin.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Picture { get; set; }
    }
}
