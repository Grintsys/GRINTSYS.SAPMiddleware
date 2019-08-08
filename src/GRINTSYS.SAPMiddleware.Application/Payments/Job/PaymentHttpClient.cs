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
                /*
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(id.ToString()), "Id");
                */
                var url = ConfigurationManager.AppSettings["SAPOrderEndpoint"] + id;
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    taskResponse = JsonConvert.DeserializeObject<TaskResponse>(apiResponse);
                }
            }
            return taskResponse;
        }
    }
}
