using BookLover.Models.BookshelfModels;
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
    public class BookshelfController : ApiController
    {
        [Authorize]

        private BookshelfService CreateBookshelfService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            BookshelfService service = new BookshelfService(userId);
            return service;
        }

        [HttpPost]
        public IHttpActionResult PostBookshelf(BookshelfCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookshelfService service = CreateBookshelfService();
            if (!service.CreateBookshelf(model))
            {
                return InternalServerError();
            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetAllBookshelves()
        {
            BookshelfService service = CreateBookshelfService();
            List<BookshelfDisplay> bookshelves = service.GetAllBookShelves();
            return Ok(bookshelves);
        }

        [HttpGet]
        public IHttpActionResult GetBookshelfById(int id)
        {
            BookshelfService service = CreateBookshelfService();
            BookshelfDisplay shelf = service.GetBookshelfById(id);
            return Ok(shelf);
        }

        [HttpPut]
        public IHttpActionResult UpdateBookshelf(BookshelfEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BookshelfService service = CreateBookshelfService();

            if (!service.UpdateBookshelf(model))
            {
                return InternalServerError();
            }
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteBookshelfById(int id)
        {
            BookshelfService service = CreateBookshelfService();
            if (!service.DeleteBookshelf(id))
            {
                return InternalServerError();
            }
            return Ok();
        }
    }
}
