using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using beheerapplicatie.viewModels;

namespace beheerapplicatie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SongViewModel songViewModel; // Declare the songViewModel field

        public MainWindow()
        {
            InitializeComponent();

            songViewModel = new SongViewModel();
            DataContext = songViewModel;
        }

        private void OnAlleSongsButtonClick(object sender, RoutedEventArgs e)
        {
            // Create an instance of the AllSongsPage
            var allSongsPage = new AllSongsPage();

            // Update the Songs property with the desired list of songs
            songViewModel.Songs = new ObservableCollection<Song>
    {
        new Song { Id = 1, Title = "Song 1", Artist = "Artist 1", Album = "Album 1" },
        new Song { Id = 2, Title = "Song 2", Artist = "Artist 2", Album = "Album 2" }
    };

            // Pass the existing SongViewModel instance to AllSongsPage
            allSongsPage.DataContext = songViewModel;

            // Navigate to the AllSongsPage within the Frame
            MainFrame.NavigationService.Navigate(allSongsPage);
        }
    }
}
