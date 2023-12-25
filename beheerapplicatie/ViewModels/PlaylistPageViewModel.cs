using beheerapplicatie.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Text.Json;

namespace beheerapplicatie.ViewModels
{
    internal class PlaylistPageViewModel : BaseViewModel
    {
        //playlist pagina wordt weergegeven 
        private MainViewModel mainViewModel;
        public PlaylistPageViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public MainViewModel MainViewModel => mainViewModel;
        //Songs worden opgehaald 
        private ObservableCollection<Song> songsList { get; set; }
        public ObservableCollection<Song> SongsList
        {
            get => songsList ?? mainViewModel.Songs;
        }
        //songs worden met een checkbox weergegeven em wordt toegevoegs aan een list SongForPlaylist
        private ObservableCollection<SongForPlaylist> songs { get; set; }
        public ObservableCollection<SongForPlaylist> Songs
        {
            get
            {
                if (songs == null)
                {
                    ObservableCollection<SongForPlaylist> songsToadd = new ObservableCollection<SongForPlaylist>();
                    foreach (Song sng in mainViewModel.Songs)
                    {
                        songsToadd.Add(new SongForPlaylist { Song = sng, IsChecked = false });
                    }
                    songs = songsToadd;
                }
                return songs;
            }
            set
            {
                songs = value;
                OnPropertyChanged();
            }
        }
        //playlist naam wordt opgehaald uit textbox 
        private string playlistName { get; set; }
        public string PlaylistName
        {
            get => playlistName;
            set
            {
                playlistName = value;
                OnPropertyChanged();
            }
        }

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

        //playlist wordt aangemaakt met de naam en de songs die zijn aangevinkt, melding als er geen songs zijn aangevinkt of als er geen naam is ingevuld
        private RelayCommand createPlaylist { get; set; }
        public RelayCommand CreatePlaylist
        {
            get => createPlaylist ?? new RelayCommand(obj =>
            {
                if (Songs.Where(s => s.IsChecked == true).Count() == 0)
                {
                    MessageBox.Show("kies minimaal 1 song");
                    return;
                }
                if (String.IsNullOrEmpty(PlaylistName))
                {
                    MessageBox.Show("Geef playlist een naam");
                    return;
                }

                mainViewModel.Playlists.Add(new Playlist()
                {
                    Name = playlistName,
                    Songs = this.Songs.Where(s => s.IsChecked == true).Select(x => x.Song).ToList()
                });
                mainViewModel.Playlists.Last().PropertyChanged += mainViewModel.Playlist_PropertyChanged;

                PlaylistName = String.Empty;
                mainViewModel.Serialize("Playlists.json", mainViewModel.Playlists);
            });
        }

        //songs worden aangevinkt als ze worden toegevoegd aan een playlist
        private RelayCommand checkBoxChecked { get; set; }
        public RelayCommand CheckBoxChecked
        {
            get => checkBoxChecked ?? new RelayCommand(obj =>
            {
                string sngName = obj as string ?? String.Empty;
                SongForPlaylist sng = Songs.Where(s => s.Song.Name == (string)obj).FirstOrDefault();
                sng.IsChecked = true;
            });
        }

        //playlist wordt verwijderd
        private RelayCommand deletePlaylist { get; set; }
        public RelayCommand DeletePlaylist
        {
            get => deletePlaylist ?? new RelayCommand(obj =>
            {
                if (!(obj is ListViewItem))
                {
                    return;
                }
                var listViewItem = obj as ListViewItem;
                mainViewModel.Playlists?.Remove(listViewItem.Content as Playlist);
                mainViewModel.Serialize("Playlists.json", mainViewModel.Playlists);
            });
        }

        //spesifiek nummer uit playlist wordt verwijderd
        private RelayCommand deleteSongFromPlaylist { get; set; }
        public RelayCommand DeleteSongFromPlaylist
        {
            get => deleteSongFromPlaylist ?? new RelayCommand(obj =>
            {
                if (!(obj is object[]))
                {
                    return;
                }
                var parameter = obj as object[];
                string songName = parameter[0] as string;
                string playlistName = parameter[1] as string;
                if (songName == null || playlistName == null)
                {
                    return;
                }

                var targetPlaylist = mainViewModel.Playlists.Where(playlist => playlist.Songs.Any(song => song.Name == songName) && playlist.Name == playlistName).FirstOrDefault();
                if (targetPlaylist != null)
                {
                    targetPlaylist.Songs.Remove(targetPlaylist.Songs.Where(s => s.Name == songName).First());
                    mainViewModel.Serialize("Playlists.json", mainViewModel.Playlists);
                    mainViewModel.Playlists = JsonSerializer.Deserialize<ObservableCollection<Playlist>>(new FileStream("Playlists.json", FileMode.OpenOrCreate, FileAccess.Read));
                }
            });
        }

        //na het maken van een playlist nog een nummer toevoegen, je kan geen nummer dubbel toevoegen
        private RelayCommand addSongToPlaylist { get; set; }
        public RelayCommand AddSongToPlaylist
        {
            get => addSongToPlaylist ?? new RelayCommand(obj =>
            {
                if (songToAdd == null)
                {
                    MessageBox.Show("Kies een song");
                    return;
                }
                if (!(obj is string))
                {
                    return;
                }
                string playlistName = obj as string;

                var targetSong = SongsList.Where(s => s.Name == songToAdd).FirstOrDefault();
                var targetPlaylist = mainViewModel.Playlists.Where(p => p.Name == playlistName).FirstOrDefault();
                if (targetPlaylist.Songs.Where(s => s.Name == targetSong.Name).Count() > 0)
                {
                    MessageBox.Show("Je kan dit nummer niet toevoegen, deze zit al in je playlist");
                    return;
                }
                if (targetPlaylist != null)
                {
                    targetPlaylist.Songs.Add(targetSong);
                    mainViewModel.Serialize("Playlists.json", mainViewModel.Playlists);
                    mainViewModel.Playlists = JsonSerializer.Deserialize<ObservableCollection<Playlist>>(new FileStream("Playlists.json", FileMode.OpenOrCreate, FileAccess.Read));
                }

                songToAdd = String.Empty;
            });
        }

        public class SongForPlaylist : NotifyPropertyChanged
        {
            public Song Song { get; set; }
            private bool isChecked { get; set; }
            public bool IsChecked
            {
                get => isChecked;
                set
                {
                    isChecked = value;
                    OnPropertyChanged();
                }
            }

        }
    }

}
