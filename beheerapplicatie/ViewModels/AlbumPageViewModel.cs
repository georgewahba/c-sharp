using beheerapplicatie.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static beheerapplicatie.ViewModels.PlaylistPageViewModel;
using System.Windows.Controls;
using System.Windows;
using System.Text.Json;

namespace beheerapplicatie.ViewModels
{
    
    internal class AlbumPageViewModel : BaseViewModel
    {
        //album pagina wordt weergegeven
        private MainViewModel mainViewModel;
        public AlbumPageViewModel(MainViewModel mainViewModel)
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
        //songs worden met een checkbox weergegeven em wordt toegevoegd aan een list SongForPlaylist
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
        //album naam wordt opgehaald uit textbox
        private string albumName { get; set; }
        public string AlbumName
        {
            get => albumName;
            set
            {
                albumName = value;
                OnPropertyChanged();
            }
        }

        //album wordt aangemaakt met de naam en de songs die zijn aangevinkt, melding als er geen songs zijn aangevinkt of als er geen naam is ingevuld
        private RelayCommand createAlbum { get; set; }
        public RelayCommand CreateAlbum
        {
            get => createAlbum ?? new RelayCommand(obj =>
            {
                if (Songs.Where(s => s.IsChecked == true).Count() == 0)
                {
                    MessageBox.Show("Kies minimaal 1 song");
                    return;
                }
                if (String.IsNullOrEmpty(AlbumName))
                {
                    MessageBox.Show("Geef album een naam");
                    return;
                }

                mainViewModel.Albums.Add(new Album()
                {
                    Name = AlbumName,
                    Songs = this.Songs.Where(s => s.IsChecked == true).Select(x => x.Song).ToList()
                });
                mainViewModel.Albums.Last().PropertyChanged += mainViewModel.Album_PropertyChanged;

                AlbumName = String.Empty;
                mainViewModel.Serialize("Albums.json", mainViewModel.Albums);
            });
        }

        //album wordt verwijderd
        private RelayCommand deleteAlbum { get; set; }
        public RelayCommand DeleteAlbum
        {
            get => deleteAlbum ?? new RelayCommand(obj =>
            {
                if (!(obj is ListViewItem))
                {
                    return;
                }
                var listViewItem = obj as ListViewItem;
                mainViewModel.Albums?.Remove(listViewItem.Content as Album);
                mainViewModel.Serialize("Albums.json", mainViewModel.Albums);
            });
        }

        //specifieke song wordt verwijderd uit album
        private RelayCommand deleteSongFromAlbum { get; set; }
        public RelayCommand DeleteSongFromAlbum
        {
            get => deleteSongFromAlbum ?? new RelayCommand(obj =>
            {
                if (!(obj is object[]))
                {
                    return;
                }
                var parameter = obj as object[];
                string songName = parameter[0] as string;
                string albumName = parameter[1] as string;
                if (songName == null || albumName == null)
                {
                    return;
                }

                var targetAlbum = mainViewModel.Albums.Where(album => album.Songs.Any(song => song.Name == songName) && album.Name == albumName).FirstOrDefault();
                if (targetAlbum != null)
                {
                    targetAlbum.Songs.Remove(targetAlbum.Songs.Where(s => s.Name == songName).First());
                    mainViewModel.Serialize("Albums.json", mainViewModel.Albums);
                    mainViewModel.Albums = JsonSerializer.Deserialize<ObservableCollection<Album>>(new FileStream("Albums.json", FileMode.OpenOrCreate, FileAccess.Read));
                }
            });
        }

        //na het maken van een album nog een nummer toevoegen, je kan geen nummer dubbel toevoegen
        private RelayCommand addSongToAlbum { get; set; }
        public RelayCommand AddSongToAlbum
        {
            get => addSongToAlbum ?? new RelayCommand(obj =>
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
                var targetAlbum = mainViewModel.Albums.Where(a => a.Name == playlistName).FirstOrDefault();
                if (targetAlbum.Songs.Where(s => s.Name == targetSong.Name).Count() > 0)
                {
                    MessageBox.Show("Je kan dit nummer niet toevoegen, deze zit al in je album");
                    return;
                }
                if (targetAlbum != null)
                {
                    targetAlbum.Songs.Add(targetSong);
                    mainViewModel.Serialize("Albums.json", mainViewModel.Albums);
                    mainViewModel.Albums = JsonSerializer.Deserialize<ObservableCollection<Album>>(new FileStream("Albums.json", FileMode.OpenOrCreate, FileAccess.Read));
                }

                songToAdd = String.Empty;
            });
        }

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
    }
}