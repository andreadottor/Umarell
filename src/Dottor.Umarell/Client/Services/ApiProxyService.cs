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

        public async ValueTask<IEnumerable<BuildingSiteModel>> GetAllBuildingSiteAsync()
        {
            var response = await _httpClient.GetAsync($"api/v1/BuildingSites");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ApiResult<IEnumerable<BuildingSiteModel>>>();
                if (data.Result)
                    return data.Data;
            }

            throw new Exception("Error on retrieve list of BuildingSites");
        }

    }
}
