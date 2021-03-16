using BookLover.Data;
using BookLover.Models.BookModels;
using BookLover.Models.GoogleBooksModels;
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
            if (!Book.ValidGenres.Select(g => g.ToLower()).ToList().Contains(model.Genre.ToLower()))
            {
                string message = "Genre must be one of the following: ";
                foreach (string genre in Book.ValidGenres)
                {
                    message += $"{genre}, ";
                }
                return BadRequest(message);
            }

            BookService service = CreateBookService();
            if (!service.CreateBook(model))
            {
                return InternalServerError();
            }
            return Ok();
        }

        [HttpPost]        
        public IHttpActionResult PostBookByGoogleId(string id)
        {
            try
            {
                BookService service = CreateBookService();
                if (!service.AddBookByGoogleId(id))
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

        [HttpPost]
        public IHttpActionResult PostBookByTitleAndAuthor(string title,string authorName)
        {
            try
            {
                BookService service = CreateBookService();
                if (!service.AddBookByTitleAndAuthor(title,authorName))
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

        [HttpPost]
        public IHttpActionResult PostBooksByAuthor(string authorFirstName,string authorLastName)
        {
            try
            {
                BookService service = CreateBookService();
                if (!service.AddBooksByAuthor(authorFirstName,authorLastName))
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



        //Ben's changes
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
        [System.Web.Http.Route("api/BooksByRating")]
        public IHttpActionResult SortBooksByRating()
        {
            BookService service = CreateBookService();
            List<BookListItem> books = service.SortBooksByRating();
            return Ok(books);
        }

        [HttpGet]
        [System.Web.Http.Route("api/BooksByGenreAndRating")]
        public IHttpActionResult SortBooksByGenreAndRating()
        {
            BookService service = CreateBookService();
            List<BookListItem> books = service.SortByGenreAndRating();
            return Ok(books);
        }

        [HttpGet]
        public IHttpActionResult GetBookById(int id)
        {
            BookService service = CreateBookService();
            BookDetail book = service.GetBookById(id);
            return Ok(book);
        }

        [HttpGet]
        [System.Web.Http.Route("api/BooksByAuthorId")]
        public IHttpActionResult GetBooksByAuthorId(int id)
        {
            BookService service = CreateBookService();
            List<BookListItem> books = service.GetBooksByAuthorId(id);
            return Ok(books);
        }

        [HttpGet]
        public IHttpActionResult GetBooksByAuthorName(string firstName, string lastName)
        {
            BookService service = CreateBookService();
            List<BookListItem> books = service.GetBooksByAuthorName(firstName, lastName);
            return Ok(books);
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


    }
}
