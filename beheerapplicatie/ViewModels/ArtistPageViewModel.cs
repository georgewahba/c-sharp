using beheerapplicatie.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace beheerapplicatie.ViewModels
{
    public class ArtistPageViewModel : BaseViewModel
    {
        //ViewModel voor de ArtistPage
        private MainViewModel mainViewModel { get; set; }
        public ArtistPageViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }
        //naam van de artiest die toegevoegd moet worden
        public MainViewModel MainViewModel { get => mainViewModel; }
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
        //functie om een artiest toe te voegen aan json, melding als de artiest al bestaat of als de naam leeg is
        private RelayCommand addArtist { get; set; }
        public RelayCommand AddArtist
        {
            get => addArtist ?? new RelayCommand(obj =>
            {
                if (mainViewModel?.Artists?.Where(x => x.Name == artistToAdd)?.Count() > 0)
                {
                    MessageBox.Show("Er is al een artiest met deze naam");
                    return;
                }
                if (String.IsNullOrEmpty(artistToAdd))
                {
                    MessageBox.Show("Naam kan niet leeg zijn");
                    return;
                }

                mainViewModel.Artists.Add(new Artist
                {
                    Name = artistToAdd,
                });
                mainViewModel.Artists.Last().PropertyChanged += mainViewModel.Artist_PropertyChanged;
                mainViewModel.Serialize("Artists.json", mainViewModel.Artists);
                ArtistToAdd = String.Empty;
            });
        }

        //artiest wordt verwijderd uit json
        private RelayCommand deleteArtist { get; set; }
        public RelayCommand DeleteArtist
        {
            get => deleteArtist ?? new RelayCommand(obj =>
            {
                Artist artistToDelete = mainViewModel.Artists.Where(a => a.Name == (string)obj).First();
                mainViewModel.Artists.Remove(artistToDelete);
                mainViewModel.Serialize("Artists.json", mainViewModel.Artists);
            });
        }
    }
}
