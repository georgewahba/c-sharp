using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asp_applicatie.Data;
using asp_applicatie.Models;

namespace asp_applicatie.Controllers
{
    public class PlaylistSongsController : Controller
    {
        private readonly asp_applicatieContext _context;

        public PlaylistSongsController(asp_applicatieContext context)
        {
            _context = context;
        }

        // GET: PlaylistSongs
        public async Task<IActionResult> Index()
        {
            var asp_applicatieContext = _context.PlaylistSong.Include(p => p.Playlist).Include(p => p.Song);
            return View(await asp_applicatieContext.ToListAsync());
        }

        // GET: PlaylistSongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlaylistSong == null)
            {
                return NotFound();
            }

            var playlistSong = await _context.PlaylistSong
                .Include(p => p.Playlist)
                .Include(p => p.Song)
                .FirstOrDefaultAsync(m => m.PlaylistSongId == id);
            if (playlistSong == null)
            {
                return NotFound();
            }

            return View(playlistSong);
        }

        // GET: PlaylistSongs/Create
        public IActionResult Create()
        {
            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "PlaylistId", "PlaylistId");
            ViewData["SongId"] = new SelectList(_context.Set<Song>(), "SongId", "SongId");
            return View();
        }

        // POST: PlaylistSongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlaylistSongId,PlaylistId,SongId,Status")] PlaylistSong playlistSong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playlistSong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "PlaylistId", "PlaylistId", playlistSong.PlaylistId);
            ViewData["SongId"] = new SelectList(_context.Set<Song>(), "SongId", "SongId", playlistSong.SongId);
            return View(playlistSong);
        }

        // GET: PlaylistSongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlaylistSong == null)
            {
                return NotFound();
            }

            var playlistSong = await _context.PlaylistSong.FindAsync(id);
            if (playlistSong == null)
            {
                return NotFound();
            }
            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "PlaylistId", "PlaylistId", playlistSong.PlaylistId);
            ViewData["SongId"] = new SelectList(_context.Set<Song>(), "SongId", "SongId", playlistSong.SongId);
            return View(playlistSong);
        }

        // POST: PlaylistSongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlaylistSongId,PlaylistId,SongId,Status")] PlaylistSong playlistSong)
        {
            if (id != playlistSong.PlaylistSongId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playlistSong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistSongExists(playlistSong.PlaylistSongId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "PlaylistId", "PlaylistId", playlistSong.PlaylistId);
            ViewData["SongId"] = new SelectList(_context.Set<Song>(), "SongId", "SongId", playlistSong.SongId);
            return View(playlistSong);
        }

        // GET: PlaylistSongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlaylistSong == null)
            {
                return NotFound();
            }

            var playlistSong = await _context.PlaylistSong
                .Include(p => p.Playlist)
                .Include(p => p.Song)
                .FirstOrDefaultAsync(m => m.PlaylistSongId == id);
            if (playlistSong == null)
            {
                return NotFound();
            }

            return View(playlistSong);
        }

        // POST: PlaylistSongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlaylistSong == null)
            {
                return Problem("Entity set 'asp_applicatieContext.PlaylistSong'  is null.");
            }
            var playlistSong = await _context.PlaylistSong.FindAsync(id);
            if (playlistSong != null)
            {
                _context.PlaylistSong.Remove(playlistSong);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaylistSongExists(int id)
        {
          return (_context.PlaylistSong?.Any(e => e.PlaylistSongId == id)).GetValueOrDefault();
        }
    }
}
