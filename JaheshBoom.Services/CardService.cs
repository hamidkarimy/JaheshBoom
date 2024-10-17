using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JaheshBoom.Services
{
    public class CardService
    {
        private readonly HttpClient _httpClient;
        private readonly string _cardInfoUrl = "https://sbxapi.izbank.ir/private/card/v1/cardInfo";

        public CardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetCardInfo(string token, string cardNumber)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var requestBody = new
            {
                cardNumber = cardNumber
            };

            var response = await _httpClient.PostAsJsonAsync(_cardInfoUrl, requestBody);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
