using System;
using System.Collections.Generic;
using System.Text;

namespace LostInLublin.Core.Models
{
    public class PostDto
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string Created_Time { get; set; }
        public DateTime CreatedTimeDate { get { return !string.IsNullOrEmpty(Created_Time) ? DateTime.Parse(Created_Time) : DateTime.Today; } }
        public string Full_Picture { get; set; }
        public string Url { get; set; }
    }
}
