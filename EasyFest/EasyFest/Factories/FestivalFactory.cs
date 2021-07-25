using EasyFest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EasyFest.Factories
{
    public class FestivalFactory
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task PrepareFestival()
        {
            var contentObject = new
            {
                query = GraphQLCommModel.QueryFestival
            };

            var content = new StringContent(JsonConvert.SerializeObject(contentObject), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44337/graphql", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var festivals = JsonConvert.DeserializeObject<ResponseData<FestivalList>>(responseString);
        }
    }

    //public class FestivalList
    //{
    //    public FestivalList()
    //    {
    //        Festivals = new List<Festival>();
    //    }

    //    [JsonProperty("festivals")]
    //    public List<Festival> Festivals { get; set; }
    //}

    //public class ResponseData<T>
    //{
    //    [JsonProperty("data")]
    //    public T Data { get; set; }
    //}
}
