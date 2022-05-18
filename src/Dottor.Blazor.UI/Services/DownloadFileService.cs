namespace Dottor.Blazor.UI.Services;

using Microsoft.JSInterop;
using System.Threading.Tasks;

public class DownloadFileService : IDownloadFileService
{
    private readonly IJSRuntime _jsRuntime;

    public DownloadFileService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task DownloadFileFromURLAsync(string fileURL, string fileName)
    {
        await _jsRuntime.InvokeVoidAsync("triggerFileDownload", fileName, fileURL);
    }

    public async Task DownloadFileFromStreamAsync(Stream fileStream, string fileName)
    {
        using var streamRef = new DotNetStreamReference(stream: fileStream);
        await _jsRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
    }

    public async Task DownloadFileFromByteArrayAsync(byte[] fileContent, string fileName, string contentType)
    {
        await _jsRuntime.InvokeVoidAsync("downloadFileFromByteArray", fileName, fileContent, contentType);
    }
}
