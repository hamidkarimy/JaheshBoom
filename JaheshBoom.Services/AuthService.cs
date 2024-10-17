using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JaheshBoom.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _authUrl = "https://sbxapi.izbank.ir/private/login/v1";

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Login(string appKey, string appSecretKey)
        {
            var requestBody = new
            {
                appKey = appKey,
                appSecretKey = appSecretKey
            };

            var response = await _httpClient.PostAsJsonAsync(_authUrl, requestBody);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody; 
        }
    }
}
