using AnimeANdGameMusic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace AnimeANdGameMusic.Controllers
{
    public class SongDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Return list of songs available in the database
        /// </summary>
        /// <returns>
        /// HEADER: 200 OK
        /// CONTENT: all the songs in the db
        /// </returns>
        // GET: api/SongData/ListSongs
        [HttpGet]
        public IEnumerable<SongDto> ListSongs()
        {
            List<Song> Songs = db.Songs
                .Include(s => s.Album)
                .Include(s => s.Album.Artist) // Include Artist linked to Album
                .Include(s => s.Genre)
                .ToList();
            // List<Song> Songs = db.Songs.Include(s => s.Album).Include(s => s.Genre).ToList(); // get the lists of songs
            List<SongDto> SongDtos = new List<SongDto>();
            // using loop , append the Song obj to the SongDto
            Songs.ForEach(s => SongDtos.Add(new SongDto()
            {
                SongId = s.SongId,
                SongTitle = s.SongTitle,
                AlbumTitle = s.Album != null ? s.Album.AlbumTitle : "Unknown Album", //check for null exceptions
                GenreTitle = s.Genre != null ? s.Genre.GenreTitle : "Unknown Genre",
                ArtistName = s.Album != null && s.Album.Artist != null ? s.Album.Artist.ArtistName : "Unknown Artist"
            }));
            return SongDtos;
        }
        /// <summary>
        /// Get a song with id 
        /// </summary>
        /// <param name="id">
        /// SongData/FindSong/5
        /// </param>
        /// <returns>
        /// HEADER: 200
        /// </returns>
        // GET: api/SongData/FindSong/5
        [ResponseType(typeof(Song))]
        [HttpGet]
        public IHttpActionResult FindSong(int id)
        {

            Song Song = db.Songs
                .Include(s => s.Album)
                    .Include(a => a.Album.Artist) // Include Artist linked to Album
                .Include(s => s.Genre)
                .SingleOrDefault(s => s.SongId == id);
            //Song Song = db.Songs.Include(s => s.Album).Include(s => s.Genre).SingleOrDefault(s => s.SongId == id); // ensures the genre and Album are parsed 
            //Song Song = db.Songs.Find(id);

            if (Song == null)
            {
                return NotFound();
            }
            SongDto songDto = new SongDto()
            {

                SongId = Song.SongId,
                SongTitle = Song.SongTitle,
                //explicit null check 
                AlbumTitle = Song.Album != null ? Song.Album.AlbumTitle : "Unknown Album",
                GenreTitle = Song.Genre != null ? Song.Genre.GenreTitle : "Unknown Genre",
                ArtistName = Song.Album != null && Song.Album.Artist != null ? Song.Album.Artist.ArtistName : "Unknown Artist"
            };

            return Ok(songDto);
        }
        /// <summary>
        /// get a song with their {id} and 
        /// update it
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="song">Song name</param>
        /// <returns>
        /// HEADER: 201
        /// </returns>
        // POST: api/SongData/UpdateSong/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateSong(int id, Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != song.SongId)
            {
                return BadRequest();
            }

            db.Entry(song).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// adds a song to the database,
        /// </summary>
        /// <param name="song"></param>
        /// <returns>
        /// HEADER:200
        /// </returns>

        // POST: api/SongData/AddSong
        [ResponseType(typeof(Song))]
        [HttpPost]
        public IHttpActionResult AddSong(Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the provided GenreId exists in the Genres table
            if (!db.Genres.Any(g => g.GenreId == song.GenreId))
            {
                // Return a BadRequest response indicating that the provided GenreId is invalid
                ModelState.AddModelError("GenreId", "Invalid GenreId. Please provide a valid GenreId.");
                return BadRequest(ModelState);
            }


            db.Songs.Add(song);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = song.SongId }, song);
        }
        /// <summary>
        /// Delete a song using the Id
        /// </summary>
        /// <param name="id">/DeleteSong/5</param>
        /// <returns>
        /// HEADER:200
        /// </returns>
        // DELETE: api/SongData/DeleteSong/5
        [ResponseType(typeof(Song))]
        [HttpPost]
        public IHttpActionResult DeleteSong(int id)
        {
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return NotFound();
            }

            db.Songs.Remove(song);
            db.SaveChanges();

            return Ok(song);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SongExists(int id)
        {
            return db.Songs.Count(e => e.SongId == id) > 0;
        }
    }
}
