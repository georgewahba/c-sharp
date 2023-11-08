using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beheerapplicatie.models
{
    internal class Album
    {
        public int Id { get; set; }
        public string AlbumTitle { get; set; }
        public List<Song> Songs { get; set; }

        public Album()
        {
            Songs = new List<Song>();
        }
    }
}
