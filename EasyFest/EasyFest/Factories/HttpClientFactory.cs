using EasyFest.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyFest.Factories
{
    public class HttpClientFactory : IHttpClientFactory
    {
        #region Init

        private static readonly HttpClient client = new HttpClient();

        #endregion

        #region Methods

        public async Task<ResponseData<T>> QueryGet<T>(string queryValue)
        {
            var contentObject = new
            {
                query = queryValue
            };

            var content = new StringContent(JsonConvert.SerializeObject(contentObject), Encoding.UTF8, "application/json");

            return await MakeRequest<T>(content);

            //var response = await client.PostAsync("https://localhost:44337/graphql", content);

            //var responseString = await response.Content.ReadAsStringAsync();
            //var responseData = JsonConvert.DeserializeObject<ResponseData<T>>(responseString);
            //return responseData;
        }

        public async Task<ResponseData<T>> MutationDo<T>(string mutationValue)
        {
            var contentObject = new
            {
                query = mutationValue
            };

            var content = new StringContent(JsonConvert.SerializeObject(contentObject), Encoding.UTF8, "application/json");

            return await MakeRequest<T>(content);

            //var response = await client.PostAsync("https://localhost:44337/graphql", content);

            //var responseString = await response.Content.ReadAsStringAsync();
            //var responseData = JsonConvert.DeserializeObject<ResponseData<T>>(responseString);
            //return responseData;
        }

        #endregion

        #region Private methods

        private async Task<ResponseData<T>> MakeRequest<T>(StringContent content)
        {
            var response = await client.PostAsync("https://localhost:44337/graphql", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseData<T>>(responseString);
            return responseData;
        }

        #endregion
    }
}
