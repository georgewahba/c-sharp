// SongManager.cs

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace beheerapplicatie.models
{
    public static class SongManager
    {
        private const string FilePath = "songs.json";

        public static List<Song> LoadSongs()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<Song>>(json);
            }
            return new List<Song>();
        }

        public static void SaveSongs(List<Song> songs)
        {
            string json = JsonConvert.SerializeObject(songs);
            File.WriteAllText(FilePath, json);
        }

        // Implement CRUD operations as needed
    }
}
