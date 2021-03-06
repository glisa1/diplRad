using EasyFest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFest.Factories
{
    public interface IHttpClientFactory
    {
        Task<ResponseData<T>> QueryGet<T>(string queryValue);

        Task<ResponseData<T>> MutationDo<T>(string mutationValue);
    }
}
