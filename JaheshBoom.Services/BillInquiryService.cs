using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JaheshBoom.Services
{
    public class BillInquiryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _billInquiryUrl = "https://sbxapi.izbank.ir/private/inquiry/v1/waterBillInquiry";

        public BillInquiryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> InquiryWaterBill(string token, string billId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var requestBody = new
            {
                billId = billId
            };

            var response = await _httpClient.PostAsJsonAsync(_billInquiryUrl, requestBody);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
