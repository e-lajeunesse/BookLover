using BookLover.Models;
using BookLover.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        
        [HttpGet]
        public IHttpActionResult Get()
        {
            AuthorServices authorService = CreateAuthorService();
            var authors = authorService.GetAuthors();
            return Ok(authors);
        }

        
        [HttpPost]
        public IHttpActionResult Post(AuthorCreate author)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateAuthorService();

            if (!service.CreateAuthor(author))
                return InternalServerError();

            return Ok();
        }

        
        [HttpGet]
        public IHttpActionResult GetAuthorById(int authorId)
        {
            AuthorServices authorService = CreateAuthorService();
            var author = authorService.GetAuthorById(authorId);
            return Ok(author);
        }

        
        [HttpDelete]
        public IHttpActionResult DeleteAuthor(int authorid)
        {
            var service = CreateAuthorService();

            if (!service.DeleteAuthor(authorid))
                return InternalServerError();

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdateAuthor(AuthorEdit model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AuthorServices service = CreateAuthorService();

            if (!service.UpdateAuthor(model))
                return InternalServerError();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetAuthorByName(string firstName, string lastName)
        {
            AuthorServices authorService = CreateAuthorService();
            AuthorDetail author = authorService.GetAuthorByName(firstName, lastName);
            return Ok(author);
        }

       

    }
}
