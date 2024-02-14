using Spotify_API.Services.Abstract;

namespace Spotify_API.Services.Concrete
{
    public class FileService : IFileService
    {
        private string _path = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("wwwroot", "musicAlbomimg"));
        private string _musicpath = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("wwwroot", "mp3"));

        // "audio/mpeg"
        public string ReadFile(string path, string template)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                template = reader.ReadToEnd();
            }
            return template;
        }
        public string UploadFile(IFormFile fromFile)
        {
            string[] supportedTypes = new[] { "jpg", "jpeg", "png" };
            var fileExt = System.IO.Path.GetExtension(fromFile.FileName).Substring(1);
            if (!supportedTypes.Contains(fileExt))
                throw new Exception("only this format / jpg,jpeg,png");

            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            FileInfo fileInfo = new FileInfo(fromFile.FileName);
            var imageName = fromFile.FileName + Guid.NewGuid().ToString() + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(_path, imageName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                fromFile.CopyTo(stream);
            }

            return imageName;
        }

        public void DeleteFile(string fileName)
        {
            var filePath = Path.Combine(_path, fileName);

            if (!File.Exists(filePath))
                return;

            File.Delete(filePath);
        }

        public string UploadMusic(IFormFile fromFile)
        {

            string[] supportedTypes = new[] { "mp3" };
            var fileExt = System.IO.Path.GetExtension(fromFile.FileName).Substring(1);
            if (!supportedTypes.Contains(fileExt))
                throw new Exception("only this format / audio/mp3");

            if (!Directory.Exists(_musicpath))
                Directory.CreateDirectory(_musicpath);

            FileInfo fileInfo = new FileInfo(fromFile.FileName);
            var music = fromFile.FileName + Guid.NewGuid().ToString() + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(_musicpath, music);


            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                fromFile.CopyTo(stream);
            }

            return music;
        }

        public void DeleteMusic(string musicUrl)
        {
            var filePath = Path.Combine(_musicpath, musicUrl);

            if (!File.Exists(filePath))
                return;

            File.Delete(filePath);
        }
    }
}
