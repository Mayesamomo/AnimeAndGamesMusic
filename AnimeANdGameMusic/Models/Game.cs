﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeANdGameMusic.Models
{
    public class Game
    {
        [Key]
        public int GameID { get; set; }
        public string GameName { get; set; }
        public int ReleaseYear { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        //Game images uploaded data for tracking
        //images will be stored into /Content/Images/Games/{id}.{extension}
        public bool GameHasPic { get; set; }
        public string PicExtension { get; set; }

        // <Genre>-<Game>  ==  1-M 
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }

    }

    public class GameDto
    {
        public int GameID { get; set; }
        public string GameName { get; set; }
        public int ReleaseYear { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int GenreId { get; set; }
        public string GenreTitle { get; set; }
        //Game images uploaded data for tracking
        //images will be stored into /Content/Images/Games/{id}.{extension}
        public bool GameHasPic { get; set; }
        public string PicExtension { get; set; }
    }
}