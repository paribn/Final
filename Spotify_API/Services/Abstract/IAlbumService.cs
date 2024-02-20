﻿using Spotify_API.DTO.Album;

namespace Spotify_API.Services.Abstract
{
    public interface IAlbumService
    {
        Task CreateAsync(AlbumPostDto albumPostDto);
        Task<List<AlbumGetDto>> GetAllAsync();
        Task<AlbumGetDetail> GetDetailAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, AlbumPutDto albumPutDto);


    }
}
