namespace Dottor.Umarell.Client.Services
{
    using Dottor.Umarell.Shared;
    using System.Net.Http.Json;

    public class ApiProxyService
    {
        private readonly HttpClient _httpClient;

        public ApiProxyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async ValueTask<bool> InsertBuildingSiteAsync(BuildingSiteInsertModel model)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/v1/BuildingSites", model);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ApiResult>();
                return data.Result;
            }

            return false;
        }

    }
}
