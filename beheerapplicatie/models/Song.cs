using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beheerapplicatie.models
{
    public class Song : NotifyPropertyChanged
    {
        //Song model met een naam en een artiest
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private Artist artist;
        public Artist Artist
        {
            get => artist;

            set
            {
                artist = value;
                OnPropertyChanged();
            }
        }
    }

}
