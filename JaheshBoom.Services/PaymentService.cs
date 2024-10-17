using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JaheshBoom.Services
{
    public class PaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _paymentUrl = "https://sbxapi.izbank.ir/private/payment/v1/initiatePayment";

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> InitiatePayment(string token, string amount, string cardNumber, string destinationNumber)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var requestBody = new
            {
                amount = amount,
                cardNumber = cardNumber,
                destinationNumber = destinationNumber
            };

            var response = await _httpClient.PostAsJsonAsync(_paymentUrl, requestBody);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
