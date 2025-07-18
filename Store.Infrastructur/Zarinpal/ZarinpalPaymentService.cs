using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Store.Infrastructure.Zarinpal
{
    public class ZarinpalPaymentService : IZarinpalPaymentService
    {
        private readonly HttpClient _httpClient;

        public ZarinpalPaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ZarinpalPaymentResponse> RequestPaymentAsync(int amount, string callbackUrl,
            string description,
            string email, string mobile)
        {
            var httpClient = new HttpClient();
            var requestData = new
            {
                Callback_url = callbackUrl,
                Description = description,
                MetaData = new { Mobile = mobile, Email = email },
                Amount = amount,
                MerchantId = "ee7dc1ff-9a7e-40ab-8b42-46c8aea2514c"
            };
            var content = new StringContent(
                JsonConvert.SerializeObject(requestData),
                Encoding.UTF8,
                "application/json");

            var response =
                await httpClient.PostAsync("https://sandbox.zarinpal.com/pg/v4/payment/request.json", content);

            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode) return null;

            // receive valid json information
            var result = JsonConvert.DeserializeObject<ZarinpalPaymentResponse>(responseString);
            return result;
        }
    }
}