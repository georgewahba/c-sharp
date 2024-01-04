using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using asp_applicatie.Models;

namespace asp_applicatie.Data
{
    public class asp_applicatieContext : DbContext
    {
        public asp_applicatieContext (DbContextOptions<asp_applicatieContext> options)
            : base(options)
        {
        }

        public DbSet<asp_applicatie.Models.Album> Album { get; set; } = default!;

        public DbSet<asp_applicatie.Models.Artist> Artist { get; set; } = default!;

        public DbSet<asp_applicatie.Models.Playlist> Playlist { get; set; } = default!;

        public DbSet<asp_applicatie.Models.PlaylistSong> PlaylistSong { get; set; } = default!;

        public DbSet<asp_applicatie.Models.Song> Song { get; set; } = default!;

        public DbSet<asp_applicatie.Models.User> User { get; set; } = default!;
    }
}
