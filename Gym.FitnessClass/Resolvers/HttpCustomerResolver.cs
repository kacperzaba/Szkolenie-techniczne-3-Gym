using System.Net.Http.Headers;
using Gym.FitnessClass.CrossCutting.Dtos;

namespace Gym.FitnessClass.Resolvers
{
    public class HttpCustomerResolver
    {
        private readonly HttpClient _client;
        public HttpCustomerResolver(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://localhost:5145/");
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            var resp = await _client.GetAsync($"api/customer/{id}");
            return resp.IsSuccessStatusCode
                ? await resp.Content.ReadFromJsonAsync<CustomerDto>()
                : null;
        }
    }
}
