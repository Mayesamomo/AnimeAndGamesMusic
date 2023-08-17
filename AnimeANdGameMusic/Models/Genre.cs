using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnimeANdGameMusic.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        [Required(ErrorMessage = "The Genre Name is required.")]
        [StringLength(50, ErrorMessage = "The Genre Title must be between 1 and 50 characters.", MinimumLength = 1)]
        public string GenreTitle { get; set; }
        //one-to-many relationship between Genre and Song
        //genre has many songs 
        public ICollection<Song> Songs { get; set; }

    }

    //dto class
    public class GenreDto
    {
        public int GenreId { get; set; }
        public string GenreTitle { get; set; }
    }
}