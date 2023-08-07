using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnimeANdGameMusic.Models
{
    public class Album
    {
        [Key] //primary key
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "The Album Name is required.")]
        [StringLength(100, ErrorMessage = "The Album Name must be between 1 and 100 characters.", MinimumLength = 1)]
        public string AlbumTitle { get; set; }

        [Required(ErrorMessage = "The Release Year is required.")]
        [Range(1900, 2100, ErrorMessage = "The Release Year must be between 1900 and 2100.")]
        public int ReleaseYear { get; set; }

        public bool HasPic { get; set; }
        public string PicExtension { get; set; }

        // one-to-many relationship between Album and Song
        public ICollection<Song> Songs { get; set; }

        //many-to-one relationship between Album and Artist
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }


    public class AlbumDto
    {
        public int AlbumId { get; set; }
        public string AlbumTitle { get; set; }
        public int ReleaseYear { get; set; }
        public bool HasPic { get; set; }
        public string PicExtension { get; set; }
        public int ArtistId { get; set; }

    }
}