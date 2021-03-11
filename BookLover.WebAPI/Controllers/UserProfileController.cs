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
        public IHttpActionResult Get()
        {
            UserProfileService userProfileService = CreateUserProfileService();
            List<UserProfileListItem> userProfiles = userProfileService.GetUserProfiles();
            return Ok(userProfiles);
        }

        [HttpPost]
        public IHttpActionResult PostUserProfile(UserProfileCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userProfileService = CreateUserProfileService();
            
            if (!userProfileService.CreateUser(model))
            {
                return InternalServerError();
            }
            return Ok();
        }
    }
}
