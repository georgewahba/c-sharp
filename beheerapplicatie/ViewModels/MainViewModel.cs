using beheerapplicatie.models;
using beheerapplicatie.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace beheerapplicatie.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public MainViewModel()
        {
            ReadStartupValues();

            // Koppel PropertyChanged events voor liedjes, artiesten, afspeellijsten en albums
            foreach (var song in songs)
            {
                song.PropertyChanged += Song_PropertyChanged;
            }
            foreach (var artist in artists)
            {
                artist.PropertyChanged += Artist_PropertyChanged;
            }
            foreach (var playlist in Playlists)
            {
                playlist.PropertyChanged += Playlist_PropertyChanged;
            }
            foreach (var album in albums)
            {
                album.PropertyChanged += Album_PropertyChanged;
            }

            // eerste pagina die opent is de Songs pagina
            frameContent = new SongPage() { DataContext = new SongPageViewModel(this) };
            SelectedFrameStr = "Songs";
        }

        // Functie om de data op te slaan in een json bestand wanneer PropertyChanged plaatsvindt
        public void Song_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Serialize("Songs.json", songs);
        }
        public void Artist_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Serialize("Artists.json", artists);
        }
        public void Playlist_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Serialize("Playlists.json", playlists);
        }
        public void Album_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Serialize("Albums.json", albums);
        }

        // Huidige inhoud van het frame
        private Page frameContent { get; set; }
        public Page FrameContent
        {
            get => frameContent;
            set
            {
                frameContent = value;
                OnPropertyChanged();
            }
        }

        // Lijst van liedjes
        private ObservableCollection<Song> songs = new ObservableCollection<Song>();
        public ObservableCollection<Song> Songs
        {
            get => songs;
            set
            {
                songs = value;
                OnPropertyChanged();
                Serialize("Songs.json", songs);
            }
        }

        // Lijst van albums
        private ObservableCollection<Album> albums { get; set; } = new ObservableCollection<Album>();
        public ObservableCollection<Album> Albums
        {
            get => albums;
            set
            {
                albums = value;
                OnPropertyChanged();
                Serialize("Albums", albums);
            }
        }

        // Lijst van artiesten
        private ObservableCollection<Artist> artists { get; set; } = new ObservableCollection<Artist>();
        public ObservableCollection<Artist> Artists
        {
            get => artists;
            set
            {
                artists = value;
                OnPropertyChanged();
                Serialize("Artists", artists);
            }
        }

        // Lijst van afspeellijsten
        private ObservableCollection<Playlist> playlists { get; set; } = new ObservableCollection<Playlist>();
        public ObservableCollection<Playlist> Playlists
        {
            get => playlists;
            set
            {
                playlists = value;
                OnPropertyChanged();
                Serialize("Playlists", playlists);
            }
        }

        // deel van het frame dat veranderd als je op een knop drukt
        private string selectedFrame { get; set; }
        public string SelectedFrameStr
        {
            get => selectedFrame;
            set
            {
                selectedFrame = value;
                OnPropertyChanged();
            }
        }

        // functie om naar de pagina met songs te gaan
        private RelayCommand songsButtonClicked { get; set; }
        public RelayCommand SongsButtonClicked
        {
            get => songsButtonClicked ?? new RelayCommand(obj =>
            {
                FrameContent = new SongPage() { DataContext = new SongPageViewModel(this) };
                SelectedFrameStr = "Songs";
            });
        }

        // functie om naar de pagina met artiesten te gaan
        private RelayCommand artistsButtonClicked { get; set; }
        public RelayCommand ArtistsButtonClicked
        {
            get => artistsButtonClicked ?? new RelayCommand(obj =>
            {
                FrameContent = new ArtistPage() { DataContext = new ArtistPageViewModel(this) };
                SelectedFrameStr = "Artists";
            });
        }

        // functie om naar de pagina met playlists te gaan
        private RelayCommand playlistsButtonClicked { get; set; }
        public RelayCommand PlaylistsButtonClicked
        {
            get => playlistsButtonClicked ?? new RelayCommand(obj =>
            {
                FrameContent = new PlaylistPage() { DataContext = new PlaylistPageViewModel(this) };
                SelectedFrameStr = "Playlists";
            });
        }

        // functie om naar de pagina met albums te gaan
        private RelayCommand albumsButtonClicked { get; set; }
        public RelayCommand AlbumsButtonClicked
        {
            get => albumsButtonClicked ?? new RelayCommand(obj =>
            {
                FrameContent = new AlbumPage() { DataContext = new AlbumPageViewModel(this) };
                SelectedFrameStr = "Albums";
            });
        }

        //JSON-bestanden ophalen
        private void ReadStartupValues()
        {
            try
            {
                songs = JsonSerializer.Deserialize<ObservableCollection<Song>>(new FileStream("Songs.json", FileMode.OpenOrCreate, FileAccess.Read));
            }
            catch (JsonException) { }

            try
            {
                albums = JsonSerializer.Deserialize<ObservableCollection<Album>>(new FileStream("Albums.json", FileMode.OpenOrCreate, FileAccess.Read));
            }
            catch (JsonException) { }

            try
            {
                artists = JsonSerializer.Deserialize<ObservableCollection<Artist>>(new FileStream("Artists.json", FileMode.OpenOrCreate, FileAccess.Read));
            }
            catch (JsonException) { }

            try
            {
                playlists = JsonSerializer.Deserialize<ObservableCollection<Playlist>>(new FileStream("Playlists.json", FileMode.OpenOrCreate, FileAccess.Read));
            }
            catch (JsonException) { }
        }
    }
}
