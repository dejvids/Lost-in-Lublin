using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LostInLublin.Core.Models
{
    public class SearchModel
    {
        public string KeyWord { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }
}
