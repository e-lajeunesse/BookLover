using BookLover.Models;
using BookLover.Models.BookReviewModels;
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
    public class BookReviewController : ApiController
    {
        [Authorize]

        private BookReviewService CreateBookReviewService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            BookReviewService bookReviewService = new BookReviewService(userId);
            return bookReviewService;
        }

        [HttpGet]
        public IHttpActionResult GetBookReviews()
        {
            BookReviewService service = CreateBookReviewService();
            List<BookReviewListItem> bookReviews = service.GetBookReviews();
            return Ok(bookReviews);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            BookReviewService bookReviewService = CreateBookReviewService();
            var bookReview = bookReviewService.GetReviewById(id);
            return Ok(bookReview);
        }

        [HttpPost]
        public IHttpActionResult PostReview(BookReviewCreate bookReview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookReviewService service = CreateBookReviewService();

            if (!service.CreateBookReview(bookReview))
            {
                return InternalServerError();
            }
            return Ok();
        }

    }
}
