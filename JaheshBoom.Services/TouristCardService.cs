using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JaheshBoom.Services
{
    public class TouristCardService
    {
        private readonly HttpClient _httpClient;
        private readonly string _touristCardUrl = "https://sbxapi.izbank.ir/private/card/v1/touristCardSaveAndIssue";

        public TouristCardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> IssueTouristCard(string token, string personalId, string firstName, string lastName)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var requestBody = new
            {
                personalId = personalId,
                firstName = firstName,
                lastName = lastName
            };

            var response = await _httpClient.PostAsJsonAsync(_touristCardUrl, requestBody);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
