using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        //song belongs to one Album
        //album has many songs
        [ForeignKey("Album")]
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }

        // many-to-one relationship between Song and Genre
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

    }

    //dto class
    public class SongDto
    {
        public int SongId { get; set; }
        public string SongTitle { get; set; }
        public String AlbumTitle { get; set; }
        public String ArtistName { get; set; }
        public String GenreTitle { get; set; }
    }
}