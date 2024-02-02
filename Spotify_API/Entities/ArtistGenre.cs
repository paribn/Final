﻿namespace Spotify_API.Entities
{
    public class ArtistGenre
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
