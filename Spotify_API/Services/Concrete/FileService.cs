using Spotify_API.Services.Abstract;

namespace Spotify_API.Services.Concrete
{
    public class FileService : IFileService
    {
        private string _path = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("wwwroot", "musicAlbomimg"));

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

    }
}
