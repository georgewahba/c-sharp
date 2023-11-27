using beheerapplicatie.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beheerapplicatie.ViewModels
{
    internal class SongViewModel : Song
    {
        private ObservableCollection<Song> songs;

        public ObservableCollection<Song> Songs
        {
            get { return songs; }
            set
            {
                if (songs != value)
                {
                    songs = value;
                    OnPropertyChanged(nameof(Songs));
                }
            }
        }

        public SongViewModel()
        {
            // placeholder songs
            Songs = new ObservableCollection<Song>
            {
                new Song { Id = 1, Title = "Song 1", Artist = "Artist 1"},
                new Song { Id = 2, Title = "Song 2", Artist = "Artist 2"},
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
