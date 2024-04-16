using NyamNyam2024.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NyamNyam2024.Converters
{
    public class RadioBtnColorForIngredientsConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IngredientOfStage stage = (IngredientOfStage)value;
            return stage.ColorRadioButtom ? "Green" : "Red";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
