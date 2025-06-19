using System.Net.Http.Headers;
using Gym.Client.CrossCutting.Dtos;

namespace Gym.Client.Resolvers
{
    public class HttpSubscriptionResolver
    {
        private readonly HttpClient _client;

        public HttpSubscriptionResolver(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://localhost:5173/");
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<SubscriptionDto> GetSubscriptionByIdAsync(int id)
        {
            var resp = await _client.GetAsync($"api/subscription/{id}");
            return resp.IsSuccessStatusCode
                ? await resp.Content.ReadFromJsonAsync<SubscriptionDto>()
                : null;
        }
    }
}
