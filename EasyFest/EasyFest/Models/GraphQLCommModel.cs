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
        public static string QueryFestival => "query {festivals { id name day month rate" +
            " festivalLocation{city state} }}";

        /// <summary>
        /// Gets query by id with details and location.
        /// </summary>
        public static string QueryFestivalDetailsWithLocation => 
            "query {festivalById(id: \"{0}\")" + 
            " { id name day month rate description" + 
            " festivalLocation{ address city state longitude latitude } " + 
            " commentsList{id commentBody createdOn user{id username}} }}";
    }
}
