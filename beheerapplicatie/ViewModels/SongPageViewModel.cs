using beheerapplicatie.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace beheerapplicatie.ViewModels
{
    public class SongPageViewModel : BaseViewModel
    {
        //SongPageViewModel met een mainViewModel, een songToAdd, een artistToAdd, een artists, een artistNames, een artistSelectionChanged, een changeArtist, een addSong, een deleteSong
        private MainViewModel mainViewModel { get; set; }
        public SongPageViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;

            artists = mainViewModel.Artists;
        }
        public MainViewModel MainViewModel => mainViewModel;
        private string songToAdd { get; set; }
        public string SongToAdd
        {
            get => songToAdd;
            set
            {
                songToAdd = value;
                OnPropertyChanged();
            }
        }

        private string artistToAdd { get; set; }
        public string ArtistToAdd
        {
            get => artistToAdd;
            set
            {
                artistToAdd = value;
                OnPropertyChanged();
            }
        }
        //Deze functie zorgt ervoor dat de artiesten worden opgehaald uit de mainViewModel
        private ObservableCollection<Artist> artists { get; set; }
        public ObservableCollection<Artist> Artists
        {
            get => artists ?? mainViewModel.Artists;
            set
            {
                artists = value;
                OnPropertyChanged();
            }
        }

        // Deze functie zorgt ervoor dat de artiest wordt veranderd
        private RelayCommand changeArtist { get; set; }
        public RelayCommand ChangeArtist
        {
            get => changeArtist ?? new RelayCommand(obj =>
            {
                string name = obj as string;
                if (name == null)
                {
                    return;
                }
                mainViewModel.Songs.Where(s => s.Name == name);
            });
        }

        //Deze functie zorgt ervoor dat er een song wordt toegevoegd en dat er een melding komt als er al een song is met dezelfde naam of als de naam leeg is
        private RelayCommand addSong { get; set; }
        public RelayCommand AddSong
        {
            get => addSong ?? new RelayCommand(obj =>
            {
                if (mainViewModel.Songs.Where(x => x.Name == songToAdd).Count() > 0)
                {
                    MessageBox.Show("Er is al een song met deze naam");
                    return;
                }
                if (String.IsNullOrEmpty(songToAdd) || String.IsNullOrEmpty(artistToAdd))
                {
                    MessageBox.Show("Naam kan niet leeg zijn");
                    return;
                }
                // dong wordt toegevoged aan JSON
                mainViewModel.Songs.Add(new Song
                {
                    Name = songToAdd,
                    Artist = mainViewModel.Artists.Where(a => a.Name == artistToAdd).FirstOrDefault()
                });
                mainViewModel.Songs.Last().PropertyChanged += mainViewModel.Song_PropertyChanged;
                mainViewModel.Serialize("Songs.json", mainViewModel.Songs);
                SongToAdd = String.Empty;
            });
        }
        //Deze functie zorgt ervoor dat er een song wordt verwijderd, en dat de song ook uit de playlists en albums wordt verwijderd
        private RelayCommand deleteSong { get; set; }
        public RelayCommand DeleteSong
        {
            get => deleteSong ?? new RelayCommand(obj =>
            {
                Song songToDelete = mainViewModel.Songs.Where(s => s.Name == (string)obj).First();
                mainViewModel.Songs.Remove(songToDelete);
                var playlistsWithSongToDelete = mainViewModel.Playlists.Where(playlist => playlist.Songs.Any(song => song.Name == songToDelete.Name)).ToList();
                var albumsWithSongToDelete = mainViewModel.Albums.Where(album => album.Songs.Any(song => song.Name == songToDelete.Name)).ToList();
                foreach (var playlist in playlistsWithSongToDelete)
                {
                    playlist.Songs.Remove(songToDelete);
                    var playlistToChange = mainViewModel.Playlists.Where(p => p.Name == playlist.Name).FirstOrDefault();
                    playlistToChange = playlist;
                }
                foreach (var album in albumsWithSongToDelete)
                {
                    album.Songs.Remove(songToDelete);
                    var albumToChange = mainViewModel.Albums.Where(a => a.Name == a.Name).FirstOrDefault();
                    albumToChange = album;
                }
                mainViewModel.Serialize("Songs.json", mainViewModel.Songs);
                mainViewModel.Serialize("Playlists.json", mainViewModel.Playlists);
                mainViewModel.Serialize("Albums.json", mainViewModel.Albums);
            });
        }
    }
}