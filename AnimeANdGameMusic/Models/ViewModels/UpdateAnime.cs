using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnimeANdGameMusic.Models.ViewModels
{
    public class UpdateAnime
    {
        //This viewmodel is a class which stores information that we need to present to /Anime/Update/{id}

        //the existing anime information

        public AnimeDto SelectedAnime { get; set; }

        // all genres to choose from when updating this anime

        public IEnumerable<GenreDto> GenreOptions { get; set; }
    }
}