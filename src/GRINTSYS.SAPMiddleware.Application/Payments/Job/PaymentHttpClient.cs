using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Payments.Job
{
    public class PaymentHttpClient
    {
        public async Task<TaskResponse> CreatePaymentOnSAP(int id)
        {
            TaskResponse taskResponse;
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(id.ToString()), "Id");

                using (var response = await httpClient.PostAsync(ConfigurationManager.AppSettings["SAPOrderEndpoint"], content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    taskResponse = JsonConvert.DeserializeObject<TaskResponse>(apiResponse);
                }
            }
            return taskResponse;
        }
    }
}
