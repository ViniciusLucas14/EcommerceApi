using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data.Services
{
    public class Sti3Service
    {
        public Sti3Service() {
        }

        public async Task SendOrderToSti3Async(String requestData)
        {
            var httpContent = new StringContent(requestData, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("email", "vlucassouza@gmail.com");
            var httpResponse = await httpClient.PostAsync("https://sti3-faturamento.azurewebsites.net/api/vendas", httpContent);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
        }
    }
}
