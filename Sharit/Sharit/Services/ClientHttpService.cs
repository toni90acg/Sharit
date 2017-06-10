using Sharit.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Sharit.Helpers;
using System.Text;

namespace Sharit.Services
{
    public class ClientHttpService
    {
        private static ClientHttpService _instance;
        public static ClientHttpService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ClientHttpService();
                }

                return _instance;
            }
        }

        //private HttpClient _client;
        public HttpClient Client { get; set; }

        public ClientHttpService()
        {
            Client = CreateClient();
        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");            
            return client;
        }

        public async Task<T> AddSharitItem<T>(T sharidItem) where T : EntityBase
        {
            var url = UrlHelper.GetUrl<T>();
            var stringContent = new StringContent(JsonConvert.SerializeObject(sharidItem), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, stringContent);
            
            
            return null;
        }

        //public async Task<IEnumerable<SharitItem>> GetSharitItems()
        //{
        //    var url = UrlHelper.GetUrl<SharitItem>();
        //    var resultMalo = await Client.GetStringAsync(GlobalSettings.AzureUrlSharitItemTable);

        //    var result = await Client.GetStringAsync(url);

        //    var sharitItems = JsonConvert.DeserializeObject<IEnumerable<SharitItem>>(result);

        //    return sharitItems;
        //}

        public async Task<IEnumerable<T>> GetItems<T>() where T : EntityBase
        {
            var url = UrlHelper.GetUrl<T>();

            var result = await Client.GetStringAsync(url);

            var items = JsonConvert.DeserializeObject<IEnumerable<T>>(result);

            return items;
        }
    }
}
