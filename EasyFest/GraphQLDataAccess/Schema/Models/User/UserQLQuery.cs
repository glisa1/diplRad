using HotChocolate;
using Storage.Services.UserService;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models.User
{
    public class UserQLQuery
    {
        #region Init

        private readonly IUserService _usersService;

        public UserQLQuery(IUserService usersService)
        {
            _usersService = usersService;
        }

        #endregion

        public IExecutable<Storage.Models.User> GetUsers()
        {
            return _usersService.GetUsers();
        }
    }
}
