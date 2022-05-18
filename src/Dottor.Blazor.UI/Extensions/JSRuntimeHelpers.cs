namespace Dottor.Blazor.UI.Extensions;

using Microsoft.JSInterop;
using System.Threading.Tasks;

public static class JSRuntimeHelpers
{
    public static async ValueTask<IJSObjectReference> ImportModuleAsync(this IJSRuntime jsRuntime, string scriptPath)
    {
        return await jsRuntime.InvokeAsync<IJSObjectReference>("import", $"{scriptPath}?v={VersionUtility.GetVersion()}");
    }

    public static async ValueTask<IJSInProcessObjectReference> ImportModuleAsync(this IJSInProcessRuntime jsInProcessRuntime, string scriptPath)
    {
        return await jsInProcessRuntime.InvokeAsync<IJSInProcessObjectReference>("import", $"{scriptPath}?v={VersionUtility.GetVersion()}");
    }
}
