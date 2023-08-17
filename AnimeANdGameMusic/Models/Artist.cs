using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnimeANdGameMusic.Models
{
    public class Artist
    {
            [Key]
            public int ArtistId { get; set; }

            [Required(ErrorMessage = "The Artist Name is required.")]
            [StringLength(100, ErrorMessage = "The Artist Name must be between 1 and 100 characters.", MinimumLength = 1)]
            public string ArtistName { get; set; }

            // one-to-many relationship between Artist and Album
            public ICollection<Album> Albums { get; set; }

        }

        //dto class
        public class ArtistDto
        {
            public int ArtistId { get; set; }
            public string ArtistName { get; set; }
        }
    }