using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AnimeANdGameMusic.Models
{
    public class Anime
    {
        [Key]
        public int AnimeID { get; set; }
        public string AnimeName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public bool AnimeHasPic { get; set; }
        public string PicExtension { get; set; }

        // <Genre>-<Anime>  ==  1-M 
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
    public class AnimeDto
    {
        public int AnimeID { get; set; }
        public string AnimeName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string FormattedDate
        {
            get
            {
                return ReleaseDate.ToString("yyyy-MM-dd");
            }
        }
        public string Description { get; set; }
        public bool AnimeHasPic { get; set; }
        public string PicExtension { get; set; }
        public int GenreId { get; set; }
        public string GenreTitle { get; set; }
    }
}