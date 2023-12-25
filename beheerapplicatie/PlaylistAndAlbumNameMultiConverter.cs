using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace beheerapplicatie
{

    public class PlaylistAndAlbumNameMultiConverter : IMultiValueConverter
    {
        //Converter die een array van objecten teruggeeft met de naam van de playlist en de naam van het album
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
