using BookLover.Models;
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
        public class AuthorController : ApiController
        {

            private AuthorServices CreateAuthorService()
            {
                var userId = Guid.Parse(User.Identity.GetUserId());
                var authorService = new AuthorServices(userId);
                return authorService;
            }

            public IHttpActionResult Get()
            {
                AuthorServices authorService = CreateAuthorService();
                var authors = authorService.GetAuthors();
                return Ok(authors);
            }
            public IHttpActionResult Post(AuthorCreate author)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var service = CreateAuthorService();

                if (!service.CreateAuthor(author))
                    return InternalServerError();

                return Ok();
            }

            public IHttpActionResult Get(int AuthorId)
            {
                AuthorServices authorService = CreateAuthorService();
                var author = authorService.GetAuthorById(AuthorId);
                return Ok(author);
            }
        }
}
