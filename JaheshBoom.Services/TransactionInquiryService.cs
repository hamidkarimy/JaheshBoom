using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JaheshBoom.Services
{
    public class TransactionInquiryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _transactionInquiryUrl = "https://sbxapi.izbank.ir/private/card/v1/touristCardTransactionInquiry";

        public TransactionInquiryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> InquiryTransactions(string token, string cardNumber)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var requestBody = new
            {
                cardNumber = cardNumber
            };

            var response = await _httpClient.PostAsJsonAsync(_transactionInquiryUrl, requestBody);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
