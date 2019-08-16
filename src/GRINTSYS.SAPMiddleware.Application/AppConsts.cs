using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GRINTSYS.SAPMiddleware
{
 
    public class AppConsts
    {
        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public const string DefaultPassPhrase = "gsKxGZ012HLL3MI5";
        public const int MaxResultCount = 50;
        public const string DefaultSortingField = "Id";
        public const double MinFilter = 0;
        public const double MaxFilter = 5000;


        public HttpClient Client;
        private static readonly Lazy<AppConsts> instance = new Lazy<AppConsts>(() => new AppConsts());

        private AppConsts() {
            Client = new HttpClient();
            Client.Timeout = TimeSpan.FromMinutes(5);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpClient GetClient()
        {
            return this.Client;
        }

        public static AppConsts Instance
        {
            get
            {
                return instance.Value;
            }
        }
    }
}
