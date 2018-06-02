using MvvmCross.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LostInLublin.Core.Converters
{
    public class DateCoverter:MvxValueConverter<DateTime,string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"Dodane: {value.ToLongDateString()}";
        }
    }
}
