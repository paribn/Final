﻿namespace Spotify_API.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ArtistGenre> ArtistGenres { get; set; }

        public List<MusicGenre> MusicGenres { get; set; }
    }
}
