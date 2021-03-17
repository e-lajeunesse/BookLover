using BookLover.Models.UserProfileModels;
using BookLover.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookLover.WebAPI.Controllers
{
    [Authorize]
    public class UserProfileController : ApiController
    {
        private UserProfileService CreateUserProfileService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var userProfileService = new UserProfileService(userId);
            return userProfileService;
        }

        [HttpGet]
        public IHttpActionResult GetAllProfiles()
        {
            UserProfileService userProfileService = CreateUserProfileService();
            List<UserProfileListItem> userProfiles = userProfileService.GetAllUserProfiles();
            return Ok(userProfiles);
        }

        [HttpGet]
        public IHttpActionResult GetUserById(int id)
        {
            UserProfileService userProfileService = CreateUserProfileService();
            UserProfileDisplay profileDisplay = userProfileService.GetUserById(id);
            return Ok(profileDisplay);
        }

        [HttpGet]
        [System.Web.Http.Route("api/GetsUserByUserId")]
        public IHttpActionResult GetUserByUserId()
        {
            UserProfileService userProfileService = CreateUserProfileService();
            UserProfileDisplay profileDisplay = userProfileService.GetUserByUserId();
            return Ok(profileDisplay);
        }

        [HttpPost]
        public IHttpActionResult PostUserProfile(UserProfileCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userProfileService = CreateUserProfileService();

            try
            {
                if (!userProfileService.CreateUser(model))
                {
                    return InternalServerError();
                }
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateUserProfile(UserProfileEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userProfileService = CreateUserProfileService();

            if (!userProfileService.UpdateUserProfile(model))
            {
                return InternalServerError();
            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult AddBookToReadList(int bookId)
        {
            UserProfileService service = CreateUserProfileService();
            if (!service.AddBookToReadList(bookId))
            {
                return InternalServerError();
            }
            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult DeleteProfileById(int id)
        {
            UserProfileService service = CreateUserProfileService();
            if (!service.DeleteUserProfile(id))
            {
                return InternalServerError();
            }
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult RemoveBookFromReadList(int bookId)
        {
            UserProfileService service = CreateUserProfileService();
            if (!service.RemoveBookFromReadList(bookId))
            {
                return InternalServerError();
            }
            return Ok();
        }
    }
}
