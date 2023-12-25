using beheerapplicatie.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace beheerapplicatie
{
    public class PlaylistAndAlbumNameConverter : IValueConverter
    {
        //Converter die de namen van de songs in een playlist of album omzet naar een lijst van strings
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<string> names = new List<string>();
            if (value is ObservableCollection<Song>)
            {
                names = ((ObservableCollection<Song>)value).Select(s => s.Name).ToList();
            }
            return names;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
