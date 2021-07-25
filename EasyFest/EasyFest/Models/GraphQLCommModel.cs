using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFest.Models
{
    public static class GraphQLCommModel
    {
        /// <summary>
        /// Gets all about festival.
        /// </summary>
        public static string QueryFestival => "query {festivals { id name day month rate }}";

        /// <summary>
        /// Gets query by id with details and location.
        /// </summary>
        public static string QueryFestivalDetailsWithLocation => "query {festivalById(id: \"{0}\") { id name day month rate festivalLocation{ address city state longitude latitude }}}";
    }
}
