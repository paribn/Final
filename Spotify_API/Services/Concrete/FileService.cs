using Spotify_API.Services.Abstract;

namespace Spotify_API.Services.Concrete
{
    public class FileService : IFileService
    {
        public string ReadFile(string path, string template)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                template = reader.ReadToEnd();
            }
            return template;
        }
    }
}
