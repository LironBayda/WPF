using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace wpf_final
{
    public class ValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            
            double num=(double)value;
            int min;
            Int32.TryParse(parameter.ToString().Split('_')[0], out min);
            int max;
            Int32.TryParse(parameter.ToString().Split('_')[1], out max);
            int len = max - min;

            if ((num-min)/len*100> 75)
            {
                return "EXTRA LARGE";
            }
            else if ((num - min) / len * 100 > 50)
                return "LARGE";
            else if ((num - min) / len * 100 > 25)
                return "MEDIUM";
            else
                return "SMALL";


        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}   

