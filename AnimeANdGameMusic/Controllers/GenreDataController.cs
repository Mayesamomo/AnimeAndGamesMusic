﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AnimeANdGameMusic.Models;

namespace Passion_Project.Controllers
{
    public class GenreDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all genres in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all genres in the database.
        /// </returns>
        /// <example>
        /// GET: api/GenreData/ListGenres
        /// </example>
        [HttpGet]
        [ResponseType(typeof(GenreDto))]
        public IEnumerable<GenreDto> ListGenres()
        {
            List<Genre> Genres = db.Genres.ToList();
            List<GenreDto> GenreDtos = new List<GenreDto>();

            Genres.ForEach(g => GenreDtos.Add(new GenreDto()
            {
                GenreId = g.GenreId,
                GenreTitle = g.GenreTitle,
            }));

            return GenreDtos;
        }

        /// <summary>
        /// Returns a particular genre in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An genre in the system matching up to the genre ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the genre</param>
        /// <example>
        /// GET: api/GenreData/FindGenre/5
        /// </example>
        [ResponseType(typeof(GenreDto))]
        [HttpGet]
        public IHttpActionResult FindGenre(int id)
        {
            Genre Genre = db.Genres.Find(id);
            GenreDto GenreDto = new GenreDto()
            {
                GenreId = Genre.GenreId,
                GenreTitle = Genre.GenreTitle,
            };
            if (Genre == null)
            {
                return NotFound();
            }

            return Ok(GenreDto);
        }

        /// <summary>
        /// Updates a particular genre in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Genre ID primary key</param>
        /// <param name="genre">JSON FORM DATA of an genre</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/GenreData/UpdateGenre/5
        /// FORM DATA: Genre JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        [Authorize]
        public IHttpActionResult UpdateGenre(int id, Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genre.GenreId)
            {
                return BadRequest();
            }

            db.Entry(genre).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
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
        /// Adds an genre to the system
        /// </summary>
        /// <param name="genre">JSON FORM DATA of an genre</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Genre ID, Genre Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/GenreData/AddGenre
        /// FORM DATA: Genre JSON Object
        /// </example>
        [ResponseType(typeof(Genre))]
        [HttpPost]
        [Authorize]
        public IHttpActionResult AddGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Genres.Add(genre);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = genre.GenreId }, genre);
        }

        /// <summary>
        /// Deletes an genre from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the genre</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/GenreData/DeleteGenre/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Genre))]
        [HttpPost]
        [Authorize]
        public IHttpActionResult DeleteGenre(int id)
        {
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return NotFound();
            }

            db.Genres.Remove(genre);
            db.SaveChanges();

            return Ok(genre);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GenreExists(int id)
        {
            return db.Genres.Count(e => e.GenreId == id) > 0;
        }
    }
}