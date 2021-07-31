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
        public static string QueryFestival => "query {festivals { id name day month rate description numberOfComments imageName" +
            " festivalLocation{city state} }}";

        /// <summary>
        /// Used to get informations about currently logged in user.
        /// </summary>
        public static string QueryLoggedUser => "query {user {id username}}";

        /// <summary>
        /// Gets query by id with details and location.
        /// {0} - id of festival
        /// </summary>
        public static string QueryFestivalDetailsWithLocation => 
            "query {festivalById(id: \"{0}\")" +
            " { id name day month rate description imageName " + 
            " festivalLocation{ address city state longitude latitude } " + 
            " commentsList{id commentBody createdOn user{id username}} }}";

        /// <summary>
        /// Gets rate given by user for festival.
        /// {0} - Festival id
        /// {1} - User id
        /// </summary>
        public static string QueryGetRateByUser =>
            "query {userRateForFestival(festivalId: \"{0}\", userId: \"{1}\")}";

        /// <summary>
        /// Given user is loged in.
        /// {0} - Username
        /// {1} - Password
        /// </summary>
        public static string MutationLogInUser =>
            "mutation {loginUser(input: {" + 
            "username: \"{0}\", " + 
            "password: \"{1}\", " +
            "clientMutationId: \"testMutationId\"" +
            "}){clientMutationId} }";

        /// <summary>
        /// Creates rate for user.
        /// {0} - Festival id
        /// {1} - User id
        /// {2} - Rate value
        /// </summary>
        public static string MutationCreateRate =>
            "mutation {createRate(input: {" +
            "festivalId: \"{0}\"" +
            "userId: \"{1}\"" +
            "rateValue: {2}" +
            "clientMutationId: \"testMutationId\"" +
            "}){festival{rate}} }";

        /// <summary>
        /// Updates rate for user.
        /// {0} - Festival id
        /// {1} - User id
        /// {2} - Rate value
        /// </summary>
        public static string MutationUpdateRate =>
            "mutation {updateRate(input: {" +
            "festivalId: \"{0}\"" +
            "userId: \"{1}\"" +
            "rateValue: {2}" +
            "clientMutationId: \"testMutationId\"" +
            "}){festival{rate}} }";

        /// <summary>
        /// Creates comment.
        /// {0} - User id
        /// {1} - Festival id
        /// {2} - Body of comment
        /// </summary>
        public static string MutationCreateComment =>
            "mutation {addComment(input: {" +
            "userId: \"{0}\", festivalId: \"{1}\", commentBody: \"{2}\"}){clientMutationId}}";
    }
}
