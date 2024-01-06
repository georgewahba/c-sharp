namespace asp_applicatie.ViewModels
{
    public class PlaylistSongViewModel
    {
        public int PlaylistId { get; set; }
        public string? PlaylistName { get; set; }
        public ICollection<SongViewModel>? songs { get; set; }
    }
}
