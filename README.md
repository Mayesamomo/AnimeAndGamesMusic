# AnimeAndGamesMusic

This is a web app built using ASP.NET Framework that allows users to explore and search for anime, retrieve information about anime songs, and discover the artists associated with those songs.

Features
--------

-   List all anime: Retrieve a list of all anime available in the database.
-   Search for anime: Search for specific anime by title, genre, or other filters.
-   Get anime songs and artist names: Retrieve a list of anime songs along with the names of the artists who performed them.
-   Get artist details: Retrieve detailed information about a specific artist, including their biography, discography, and social media profiles.
-   List artists and the anime they sang in: View a list of artists and the anime series they contributed songs to.

Technologies Used
-----------------

-   ASP.NET Framework: The primary framework used for the backend development.
-   C#: The programming language used to write the server-side code.
-   Entity Framework: An object-relational mapper used for database management.
-   SQL Server: The relational database management system for storing anime, songs, and artist data.
-   HTML, CSS, and JavaScript: The core technologies used for the frontend development.
-   Bootstrap: A frontend framework used for designing responsive and user-friendly interfaces.

API Endpoints
-------------

-   `GET /api/anime`: Retrieves a list of all anime.
-   `GET /api/anime/{id}`: Retrieves detailed information about a specific anime.
-   `GET /api/anime/search?q={query}`: Searches for anime based on the provided query.
-   `GET /api/anime/{id}/songs`: Retrieves a list of songs for a specific anime.
-   `GET /api/artists/{id}`: Retrieves detailed information about a specific artist.
-   `GET /api/artists`: Retrieves a list of all artists.
-   `GET /api/artists/{id}/anime`: Retrieves a list of anime that an artist has contributed songs to.
