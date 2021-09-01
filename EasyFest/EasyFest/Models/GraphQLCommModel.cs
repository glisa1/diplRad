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
        //public static string QueryFestival => "query {festivals { id name day month rate description numberOfComments images" +
        //    " city state }}";

        /// <summary>
        /// Gets all about festival.
        /// </summary>
        public static string QueryFestival => "query {festivals(first: 10){ edges{ cursor node{ id name day month rate description numberOfComments images" +
            " city state}} pageInfo{hasNextPage} totalCount }}";

        /// <summary>
        /// Gets all festivals by term.
        /// {0} - festival name (term)
        /// {1} - festival description (term)
        /// </summary>
        public static string QueryFestivalSearch => "query {festivals(first: 10, where:{or: [{name: {contains: \"{0}\"}}, {name: {contains: \"{1}\"}}, {description: {contains: \"{2}\"}}, {description: {contains: \"{3}\"}}]}){ edges{ " + 
            " cursor node{ id name day month rate description numberOfComments images" +
            " city state}} pageInfo{hasNextPage} totalCount }}";

        /// <summary>
        /// Used to get informations about currently logged in user.
        /// </summary>
        public static string QueryLoggedUser => "query {user {id username isAdmin}}";

        /// <summary>
        /// Gets query by id with details and location.
        /// {0} - id of festival
        /// </summary>
        public static string QueryFestivalDetailsWithLocation => 
            "query {festivalById(id: \"{0}\")" +
            " { id name day month rate description images endDay endMonth " + 
            " address city state longitude latitude " + 
            " commentsList{id commentBody createdOn user{id username imageId}} }}";

        /// <summary>
        /// Gets query by id with details and location.
        /// {0} - id of festival
        /// </summary>
        public static string QueryFestivalImages =>
            "query {festivalById(id: \"{0}\")" +
            " { images }}";

        /// <summary>
        /// Gets rate given by user for festival.
        /// {0} - Festival id
        /// {1} - User id
        /// </summary>
        public static string QueryGetRateByUser =>
            "query {userRateForFestival(festivalId: \"{0}\", userId: \"{1}\")}";

        /// <summary>
        /// Gets user by id.
        /// {0} - User id
        /// </summary>
        public static string QueryGetUserById =>
            "query {userById(id: \"{0}\"){id username email commentsPostedByUser ratesGivenByUser imageId}}";

        /// <summary>
        /// Gets user by id.
        /// {0} - User id
        /// </summary>
        public static string QueryGetIsUserAdmin =>
            "query {userById(id: \"{0}\"){isAdmin}}";

        /// <summary>
        /// Gets list of festivals for festivals map.
        /// </summary>
        public static string QueryGetFestivalMap =>
            "query{festivals: festivalsInfo{id name rate numberOfComments latitude longitude images}}";

        /// <summary>
        /// Gets query by id with details and location.
        /// {0} - id of festival
        /// </summary>
        public static string QueryGetFestivalForEdit =>
            "query {festivalById(id: \"{0}\")" +
            " { id name day month description images endDay endMonth " +
            " address city state longitude latitude " +
            " }}";

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
        /// Creates user.
        /// {0} - Username
        /// {1} - Password
        /// {2} - Email
        /// </summary>
        public static string MutationCreateUser =>
            "mutation {addUser(input: {" +
            "username: \"{0}\", " +
            "password: \"{1}\", " +
            "email: \"{2}\", " +
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

        /// <summary>
        /// Deletes given user.
        /// {0} - User id
        /// </summary>
        public static string MutationDeleteUser =>
            "mutation {deleteUser(userId: \"{0}\")}";

        /// <summary>
        /// Deletes given festival.
        /// {0} - Festival id
        /// </summary>
        public static string MutationDeleteFestival =>
            "mutation {deleteFestival(festivalId: \"{0}\")}";


        /// <summary>
        /// Creates new festival.
        /// {0} - name,
        /// {1} - month,
        /// {2} - day,
        /// {3} - endMonth,
        /// {4} - endDay,
        /// {5} - description,
        /// {6} - images,
        /// {7} - latitude,
        /// {8} - longitude,
        /// {9} - address,
        /// {10} - city,
        /// {11} - state,
        /// {12} - clientMutationId
        /// </summary>
        public static string MutationCreateFestival =>
            "mutation {createFestival(input: {" + 
            " name: \"{0}\", " +
            " month: {1}, " +
            " day: {2}, " +
            " endMonth: {3}, " +
            " endDay: {4}, " +
            " description: \"{5}\", " +
            " images: \"{6}\", " +
            " latitude: {7}, " +
            " longitude: {8}, " +
            " address: \"{9}\", " +
            " city: \"{10}\", " +
            " state: \"{11}\", " +
            " clientMutationId: \"{12}\", " +
            "}){clientMutationId}}";


        /// <summary>
        /// Updates the festival.
        /// {0} - name,
        /// {1} - month,
        /// {2} - day,
        /// {3} - endMonth,
        /// {4} - endDay,
        /// {5} - description,
        /// {6} - images,
        /// {7} - latitude,
        /// {8} - longitude,
        /// {9} - address,
        /// {10} - city,
        /// {11} - state,
        /// {12} - clientMutationId
        /// </summary>
        public static string MutationUpdateFestival =>
            "mutation {updateFestival(input: {" +
            " name: \"{0}\", " +
            " month: {1}, " +
            " day: {2}, " +
            " endMonth: {3}, " +
            " endDay: {4}, " +
            " description: \"{5}\", " +
            " images: \"{6}\", " +
            " latitude: {7}, " +
            " longitude: {8}, " +
            " address: \"{9}\", " +
            " city: \"{10}\", " +
            " state: \"{11}\", " +
            " clientMutationId: \"{12}\", " +
            " festivalId: \"{13}\", " +
            " checkName: {14} " +
            "}){clientMutationId}}";

    }
}
