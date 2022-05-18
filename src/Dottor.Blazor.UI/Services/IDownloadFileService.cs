namespace Dottor.Blazor.UI.Services
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IDownloadFileService
    {
        Task DownloadFileFromStreamAsync(Stream fileStream, string fileName);
        Task DownloadFileFromURLAsync(string fileURL, string fileName);
        Task DownloadFileFromByteArrayAsync(byte[] fileContent, string fileName, string contentType);
    }
}