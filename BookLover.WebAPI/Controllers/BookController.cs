using BookLover.Models.BookModels;
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
    public class BookController : ApiController
    {
        [Authorize]

        private BookService CreateBookService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            BookService service = new BookService(userId);
            return service;
        }

        [HttpPost]
        public IHttpActionResult PostBook(BookCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookService service = CreateBookService();
            if (!service.CreateBook(model))
            {
                return InternalServerError();
            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetBooks()
        {
            BookService service = CreateBookService();
            List<BookListItem> books = service.GetAllBooks();
            return Ok(books);
        }

        [HttpGet]
        public IHttpActionResult GetBooksByGenre(string genre)
        {
            BookService service = CreateBookService();
            List<BookListItem> books = service.GetBooksByGenre(genre);
            if (books.Count > 0)
            {
                return Ok(books);
            }
            return Ok($"No books found for genre: {genre}");
        }

        [HttpGet]
        public IHttpActionResult GetBookById(int id)
        {
            BookService service = CreateBookService();
            BookDetail book = service.GetBookById(id);
            return Ok(book);
        }

        [HttpGet]
        public IHttpActionResult GetBookByTitle(string title)
        {
            BookService service = CreateBookService();
            BookDetail book = service.GetBookByTitle(title);
            return Ok(book);
        }

        [HttpPut]
        public IHttpActionResult UpdateBook(BookEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookService service = CreateBookService();
            if (!service.UpdateBook(model))
            {
                return InternalServerError();
            }
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteBook(int id)
        {
            BookService service = CreateBookService();
            if (!service.DeleteBook(id))
            {
                return InternalServerError();
            }
            return Ok();
        }

        //Ben's changes
        [HttpGet]
        public IHttpActionResult GetBooksByAuthor()
        {
            BookService service = CreateBookService();
            List<BookListItem> books = service.GetBooksByAuthor();
            return Ok(books);
        }
    }
}
