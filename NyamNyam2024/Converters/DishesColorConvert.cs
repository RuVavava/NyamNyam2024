using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using NyamNyam2024.DB;


namespace NyamNyam2024.Converters
{
    public class DishesColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Dish dish = value as Dish;
            return (bool)dish.Availble ? PixelFormats.Pbgra32 : PixelFormats.Gray8;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
