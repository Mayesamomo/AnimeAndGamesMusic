using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AnimeANdGameMusic.Models;
using System.Diagnostics;
using System.IO;    // needed for updating pictures 
using System.Web;

namespace AnimeANdGameMusic.Controllers
{
    public class GameDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <returns> all games in the database</returns>
        /// GET: api/GameData/ListGames
        [HttpGet]
        [ResponseType(typeof(GameDto))]
        public IHttpActionResult ListGames()
        {
            List<Game> Games = db.Games.ToList();
            List<GameDto> GameDtos = new List<GameDto>();

            // loop through the database game table to get all information
            Games.ForEach(a => GameDtos.Add(new GameDto()
            {
                GameID = a.GameID,
                GameName = a.GameName,
                ReleaseYear = a.ReleaseYear,
                Description = a.Description,
                Price = a.Price,
                GenreId = a.Genre.GenreId,
                GenreTitle = a.Genre.GenreTitle,
                GameHasPic = a.GameHasPic,
                PicExtension = a.PicExtension,
            }));
            return Ok(GameDtos);
        }

        /// Get information about all games related to a particular genre ID
        /// For simple design, one game only has one genre type, but one genre can have many games.  1--M relationship
        /// <returns>
        ///  all games in the database, including their associated genre matched with a particular genre ID
        /// </returns>
        /// <param name="id">Genre ID.</param>
        /// api/GameData/ListgamesForGenre/2
        [HttpGet]
        [ResponseType(typeof(GameDto))]
        public IHttpActionResult ListGamesForGenre(int id)
        {
            List<Game> Games = db.Games.Where(a => a.GenreId == id).ToList();
            List<GameDto> GameDtos = new List<GameDto>();

            Games.ForEach(a => GameDtos.Add(new GameDto()
            {
                GameID = a.GameID,
                GameName = a.GameName,
                ReleaseYear = a.ReleaseYear,
                Description = a.Description,
                Price = a.Price,
                GenreId = a.Genre.GenreId,
                GenreTitle = a.Genre.GenreTitle,
                GameHasPic = a.GameHasPic,
                PicExtension = a.PicExtension,
            }));

            return Ok(GameDtos);
        }

        /// Get all Games in the system.
        /// <param name="id">The primary key of the Game</param>
        /// GET: api/GamesData/FindGame/5
        [HttpGet]
        [ResponseType(typeof(GameDto))]
        public IHttpActionResult FindGame(int id)
        {
            Game Game = db.Games.Find(id);
            GameDto GameDto = new GameDto()
            {
                GameID = Game.GameID,
                GameName = Game.GameName,
                ReleaseYear = Game.ReleaseYear,
                Description = Game.Description,
                Price = Game.Price,
                GenreId = Game.Genre.GenreId,
                GenreTitle = Game.Genre.GenreTitle,
                GameHasPic = Game.GameHasPic,
                PicExtension = Game.PicExtension,
            };
            if (Game == null)
            {
                return NotFound();
            }

            return Ok(GameDto);
        }

        /// Update a particular Game
        /// <param name="id">Game ID primary key</param>
        /// <param name="Game">Game json data</param>
        /// PUT: api/GamesData/UpdateGame/5
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateGame(int id, Game Game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Game.GameID)
            {
                return BadRequest();
            }

            db.Entry(Game).State = EntityState.Modified;
            db.Entry(Game).Property(a => a.GameHasPic).IsModified = false;
            db.Entry(Game).Property(a => a.PicExtension).IsModified = false;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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
        /// upload to the server, set GameHasPic status
        /// POST: api/GameData/UpdateGamePic/1
        [HttpPost]
        public IHttpActionResult UploadGamePic(int id)
        {

            bool haspic = false;
            string picextension;
            if (Request.Content.IsMimeMultipartContent())
            {
                Debug.WriteLine("Received multipart form data.");

                int numfiles = HttpContext.Current.Request.Files.Count;
                Debug.WriteLine("Files Received: " + numfiles);

                //Check if a file is posted
                if (numfiles == 1 && HttpContext.Current.Request.Files[0] != null)
                {
                    var gamePic = HttpContext.Current.Request.Files[0];
                    //Check if the file is empty
                    if (gamePic.ContentLength > 0)
                    {
                        //establish valid file types (can be changed to other file extensions if desired!)
                        var valtypes = new[] { "jpeg", "jpg", "png", "gif" };
                        var extension = Path.GetExtension(gamePic.FileName).Substring(1);
                        //Check the extension of the file
                        if (valtypes.Contains(extension))
                        {
                            try
                            {
                                //file name is the id of the image
                                string fn = id + "." + extension;

                                //get a direct file path to ~/Content/animes/{id}.{extension}
                                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Images/Games/"), fn);

                                //save the file
                                gamePic.SaveAs(path);

                                //if these are all successful then we can set these fields
                                haspic = true;
                                picextension = extension;

                                //Update the anime haspic and picextension fields in the database
                                Game Selectedgame = db.Games.Find(id);
                                Selectedgame.GameHasPic = haspic;
                                Selectedgame.PicExtension = extension;
                                db.Entry(Selectedgame).State = EntityState.Modified;

                                db.SaveChanges();

                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("Game Image was not saved successfully.");
                                Debug.WriteLine("Exception:" + ex);
                                return BadRequest();
                            }
                        }
                    }

                }

                return Ok();
            }
            else
            {
                //not multipart form data
                return BadRequest();

            }

        }


        /// Add a new Game
        /// <param name="Game">Game json data</param>
        /// POST: api/GameData/AddGame
        [HttpPost]
        [ResponseType(typeof(Game))]
        public IHttpActionResult AddGame(Game Game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Games.Add(Game);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Game.GameID }, Game);
        }

        /// Delete a Game
        /// DELETE: api/GamesData/DeleteGame/5
        [HttpPost]
        [ResponseType(typeof(Game))]
        public IHttpActionResult DeleteGame(int id)
        {
            Game Game = db.Games.Find(id);
            if (Game == null)
            {
                return NotFound();
            }
            if (Game.GameHasPic && Game.PicExtension != "")
            {
                //delete game picture from path
                string path = HttpContext.Current.Server.MapPath("~/Content/Images/Games/" + id + "." + Game.PicExtension);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            db.Games.Remove(Game);
            db.SaveChanges();

            return Ok(Game);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameExists(int id)
        {
            return db.Games.Count(e => e.GameID == id) > 0;
        }
    }
}