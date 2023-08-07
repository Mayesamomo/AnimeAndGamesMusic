using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnimeANdGameMusic.Models
{
    public class Song
    {

        [Key] //primary key
        public int SongId { get; set; }

        [Required(ErrorMessage = "The Song Title is required.")]
        [StringLength(100, ErrorMessage = "The Song Title must be between 1 and 100 characters.", MinimumLength = 1)]
        public string SongTitle { get; set; }


        // many-to-one relationship between Song and Album
        public int AlbumId { get; set; }
        public Album Album { get; set; }

        // many-to-one relationship between Song and Genre
        public int GenreId { get; set; }
        public Genre Genre { get; set; }


    }

    //dto class
    public class SongDto
    {
        public int SongId { get; set; }
        public string SongTitle { get; set; }
        public int AlbumId { get; set; }
        public int GenreId { get; set; }
    }
}