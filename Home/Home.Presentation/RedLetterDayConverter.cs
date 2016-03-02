using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Home.Presentation
{
    public class RedLetterDayConverter : IValueConverter
    {
        static Dictionary<DateTime, string> dict = new Dictionary<DateTime, string>();

        static RedLetterDayConverter()
        {
            dict.Add(new DateTime(2016, 2, 20), "St. Patrick's Day");
 
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text;
            if (!dict.TryGetValue((DateTime)value, out text))
                text = null;
            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
