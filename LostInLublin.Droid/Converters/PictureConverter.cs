using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Converters;

namespace LostInLublin.Droid.Converters
{
    class PictureConverter:MvxValueConverter<string,string>
    {
        protected override string Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
           return string.IsNullOrEmpty(value) ? "@drawable/place.png" : value;
        }
    }
}