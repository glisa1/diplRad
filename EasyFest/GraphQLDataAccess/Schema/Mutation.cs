using GraphQLDataAccess.Schema.Models;
using HotChocolate;
using HotChocolate.Execution;
using Storage.Models;
using Storage.Services.AuthenticationService;
using Storage.Services.CommentsService;
using Storage.Services.FestivalLocationsService;
using Storage.Services.FestivalService;
using Storage.Services.RateService;
using Storage.Services.UserService;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLDataAccess.Schema
{
    public class Mutation
    {
        #region Init

        #region Fields

        private readonly IFestivalService _festivalService;
        private readonly IFestivalLocationsService _locationService;
        private readonly IRateService _rateService;
        private readonly ICommentsService _commentService;
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authService;

        #endregion

        public Mutation(IFestivalService festivalService,
                                IFestivalLocationsService locationsService,
                                IRateService rateService,
                                ICommentsService commentsService,
                                IUserService userService,
                                IAuthenticationService authenticationService)
        {
            _festivalService = festivalService;
            _locationService = locationsService;
            _rateService = rateService;
            _commentService = commentsService;
            _userService = userService;
            _authService = authenticationService;
        }

        #endregion

        // TIPS: da bi se videla metoda u mutaciji mora imati neku povratnu vrednost uz task

        #region Methods

        #region Comment mutations

        public async Task<CommentCreatedPayload> AddComment(CreateCommentInput input)
        {
            if (string.IsNullOrEmpty(input.CommentBody))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Username cannot be empty.")
                        .SetCode("USERNAME_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.FestivalId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The festivalId cannot be empty.")
                        .SetCode("FESTIVALID_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.UserId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The userId cannot be empty.")
                        .SetCode("USERID_EMPTY")
                        .Build());
            }

            var festival = await _festivalService.GetFestivalByIdAsync(input.FestivalId);

            if (festival == null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Festival not found.")
                        .SetCode("FESTIVAL_NOT_FOUND")
                        .Build());
            }

            var user = await _userService.GetUserWithIdAsync(input.UserId);

            if (user == null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("User not found.")
                        .SetCode("USER_NOT_FOUND")
                        .Build());
            }

            var newComment = new Comment
            {
                CommentBody = input.CommentBody,
                UserId = input.UserId,
                FestivalId = input.FestivalId,
                CreatedOn = DateTime.Now,
                Festival = festival,
                User = user
            };

            await _commentService.InsertCommentAsync(newComment);

            return new CommentCreatedPayload(newComment, input.ClientMutationId);
        }

        public async Task<CommentCreatedPayload> UpdateComment(UpdateCommentInput input)
        {
            if (string.IsNullOrEmpty(input.CommentBody))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Username cannot be empty.")
                        .SetCode("USERNAME_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.CommentId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The commentId cannot be empty.")
                        .SetCode("COMMENTID_EMPTY")
                        .Build());
            }

            var newComment = await _commentService.GetCommentById(input.CommentId);
            newComment.CommentBody = input.CommentBody;

            await _commentService.UpdateCommentAsync(newComment);

            return new CommentCreatedPayload(newComment, input.ClientMutationId);
        }

        public async Task<bool> DeleteComment(string commentId)
        {
            if (string.IsNullOrEmpty(commentId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The commentId cannot be empty.")
                        .SetCode("COMMENTID_EMPTY")
                        .Build());
            }

            await _commentService.DeleteCommentAsync(commentId);

            return true;
        }

        #endregion

        #region Festival locations mutations

        public async Task<FestivalLocationCreatedPayload> CreateFestivalLocation(CreateFestivalLocationInput input)
        {
            if (string.IsNullOrEmpty(input.FestivalId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The festivalId cannot be empty.")
                        .SetCode("FESTIVALID_EMPTY")
                        .Build());
            }


            var festLocation = await _locationService.GetFestivalLocationAsync(input.FestivalId);
            if (festLocation != null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Location for this festival already exists.")
                        .SetCode("LOCATION_ALREADY_EXISTS")
                        .Build());
            }

            var newFestivalLocation = new FestivalLocation
            {
                Address = input.Address,
                City = input.City,
                FestivalId = input.FestivalId,
                Latitude = input.Latitude,
                Longitude = input.Longitude,
                State = input.State
            };

            await _locationService.InsertFestivalLocationAsync(newFestivalLocation);

            return new FestivalLocationCreatedPayload(newFestivalLocation, input.ClientMutationId);
        }

        public async Task<FestivalLocationCreatedPayload> UpdateFestivalLocation(UpdateFestivalLocationInput input)
        {
            if (string.IsNullOrEmpty(input.FestivalLocationId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The festivalId cannot be empty.")
                        .SetCode("FESTIVALID_EMPTY")
                        .Build());
            }

            var newFestivalLocation = new FestivalLocation
            {
                Id = input.FestivalLocationId,
                Address = input.Address,
                City = input.City,
                Latitude = input.Latitude,
                Longitude = input.Longitude,
                State = input.State
            };

            // Razmisliti da se koristi model za create jer on vraca i festival i festivalId

            await _locationService.UpdateFestivalLocationAsync(newFestivalLocation);

            return new FestivalLocationCreatedPayload(newFestivalLocation, input.ClientMutationId);
        }

        public async Task<bool> DeleteFestivalLocation(string festivalLocationId)
        {
            if (string.IsNullOrEmpty(festivalLocationId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The festivalId cannot be empty.")
                        .SetCode("FESTIVALID_EMPTY")
                        .Build());
            }

            await _locationService.DeleteFestivalLocationAsync(festivalLocationId);

            return true;
        }

        #endregion

        #region Festival mutations

        public async Task<FestivalCreatedPayload> CreateFestival(CreateFestivalInput input)
        {
            if (string.IsNullOrEmpty(input.Name))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Name cannot be empty.")
                        .SetCode("NAME_EMPTY")
                        .Build());
            }

            var isNameTaken = await _festivalService.GetFestivalByName(input.Name);

            if (isNameTaken)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Festival with given name already exists.")
                        .SetCode("FESTIVAL_NAME_EXISTS")
                        .Build());
            }

            var newFestival = new Festival
            {
                Name = input.Name,
                Day = input.Day,
                Month = input.Month
            };

            await _festivalService.InsertFestivalAsync(newFestival);

            return new FestivalCreatedPayload(newFestival, input.ClientMutationId);
        }

        public async Task<FestivalCreatedPayload> UpdateFestival(UpdateFestivalInput input)
        {
            if (string.IsNullOrEmpty(input.FestivalId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("FestivalId cannot be empty.")
                        .SetCode("FESTIVALID_EMPTY")
                        .Build());
            }

            if (!string.IsNullOrEmpty(input.Name))
            {
                var isNameTaken = await _festivalService.GetFestivalByName(input.Name);

                if (isNameTaken)
                {
                    throw new QueryException(
                        ErrorBuilder.New()
                            .SetMessage("Festival with given name already exists.")
                            .SetCode("FESTIVAL_NAME_EXISTS")
                            .Build());
                }
            }

            var newFestival = new Festival
            {
                Id = input.FestivalId,
                Name = input.Name,
                Day = input.Day,
                Month = input.Month
            };

            await _festivalService.UpdateFestivalAsync(newFestival);

            return new FestivalCreatedPayload(newFestival, input.ClientMutationId);
        }

        /// <summary>
        /// Deletes festival and all adjacent data.
        /// </summary>
        /// <param name="festivalId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteFestival(string festivalId)
        {
            //We call each service that deletes adjacent data.
            await _locationService.DeleteFestivalLocationByFestivalIdAsync(festivalId);
            await _rateService.DeleteRatesByFestivalidAsync(festivalId);
            await _commentService.DeleteCommentsByFestivalIdAsync(festivalId);
            await _festivalService.DeleteFestivalAsync(festivalId);

            return true;
        }

        #endregion

        #region Rate mutations

        public async Task<RateCreatedPayload> CreateRate(CreateRateInput input)
        {
            if (input.RateValue < 0 || input.RateValue > 5)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Rate value is incorrect.")
                        .SetCode("BAD_RATEVALUE")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.FestivalId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The festivalId cannot be empty.")
                        .SetCode("FESTIVALID_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.UserId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The userId cannot be empty.")
                        .SetCode("USERID_EMPTY")
                        .Build());
            }

            var festival = await _festivalService.GetFestivalByIdAsync(input.FestivalId);

            if (festival == null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Festival not found.")
                        .SetCode("FESTIVAL_NOT_FOUND")
                        .Build());
            }

            var user = await _userService.GetUserWithIdAsync(input.UserId);

            if (user == null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("User not found.")
                        .SetCode("USER_NOT_FOUND")
                        .Build());
            }

            var newRate = new Rate
            {
                RateValue = input.RateValue,
                FestivalId = input.FestivalId,
                UserId = input.UserId,
                Festival = festival,
                User = user
            };

            await _rateService.InsertRateAsync(newRate);

            return new RateCreatedPayload(newRate, festival, input.ClientMutationId);
        }

        public async Task<RateCreatedPayload> UpdateRate(CreateRateInput input)
        {
            if (input.RateValue < 0 || input.RateValue > 5)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Rate value is incorrect.")
                        .SetCode("BAD_RATEVALUE")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.FestivalId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The festivalId cannot be empty.")
                        .SetCode("FESTIVALID_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.UserId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The userId cannot be empty.")
                        .SetCode("USERID_EMPTY")
                        .Build());
            }

            var festival = await _festivalService.GetFestivalByIdAsync(input.FestivalId);

            if (festival == null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Festival not found.")
                        .SetCode("FESTIVAL_NOT_FOUND")
                        .Build());
            }

            var user = await _userService.GetUserWithIdAsync(input.UserId);

            if (user == null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("User not found.")
                        .SetCode("USER_NOT_FOUND")
                        .Build());
            }

            var newRate = new Rate
            {
                RateValue = input.RateValue,
                FestivalId = input.FestivalId,
                UserId = input.UserId,
                Festival = festival,
                User = user
            };

            await _rateService.UpdateRateAsync(newRate);


            return new RateCreatedPayload(newRate, festival, input.ClientMutationId);
        }

        #endregion

        #region User mutations

        public async Task<UserCreatedPayload> AddUser(CreateUserInput input)
        {
            if (string.IsNullOrEmpty(input.Username))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Username cannot be empty.")
                        .SetCode("USERNAME_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.Email))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The email cannot be empty.")
                        .SetCode("EMAIL_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.Password))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The password cannot be empty.")
                        .SetCode("PASSWORD_EMPTY")
                        .Build());
            }

            string salt = Guid.NewGuid().ToString("N");

            byte[] hash;
            using (var sha = SHA512.Create())
            {
                hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + salt));
            }

            User model = new User
            {
                Email = input.Email,
                Password = Convert.ToBase64String(hash),
                Salt = salt,
                Username = input.Username
            };

            await _userService.AddUserAsync(model);

            return new UserCreatedPayload(model, input.ClientMutationId);
        }

        public async Task<LoginPayload> LoginUser(LoginInput input)
        {
            if (string.IsNullOrEmpty(input.Username))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The username mustn not be empty.")
                        .SetCode("USERNAME_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.Password))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The password mustn't be empty.")
                        .SetCode("PASSWORD_EMPTY")
                        .Build());
            }

            var user = await _userService.GetUserWithUsernameAsync(input.Username);

            if (user is null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The specified username or password are invalid.")
                        .SetCode("INVALID_CREDENTIALS")
                        .Build());
            }

            byte[] hash;
            using (var sha = SHA512.Create())
            {
                hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + user.Salt));
            }

            if (!Convert.ToBase64String(hash).Equals(user.Password, StringComparison.Ordinal))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The specified username or password are invalid.")
                        .SetCode("INVALID_CREDENTIALS")
                        .Build());
            }

            await _authService.SignInAsync(user, true);

            return new LoginPayload(input.ClientMutationId);
        }

        public async Task DeleteUser(string userId)
        {
            await _userService.DeleteUserAsync(userId);
        }

        #endregion

        #endregion
    }
}
