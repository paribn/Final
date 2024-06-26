﻿namespace Spotify_API.Services.Abstract
{
    public interface IFileService
    {
        string ReadFile(string path, string template);

        string UploadFile(IFormFile fromFile);

        public void DeleteFile(string fileName);
        string UploadMusic(IFormFile fromFile);

        public void DeleteMusic(string musicUrl);



    }
}
