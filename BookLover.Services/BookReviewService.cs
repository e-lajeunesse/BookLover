using BookLover.Data;
using BookLover.Models;
using BookLover.Models.BookModels;
using BookLover.Models.BookReviewModels;
using BookLover.Models.CommentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Services
{
    public class BookReviewService
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        
        private readonly Guid _userId;
        public BookReviewService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateBookReview(BookReviewCreate model)
        {
            BookReview bookReview = new BookReview()
            {
                OwnerId = _userId,
                ReviewText = model.ReviewText,
                BookRating = model.BookRating,
                ReviewTitle = model.ReviewTitle,
                BookId = model.BookId,
                CreatedReview = DateTimeOffset.Now
            };

            _context.BookReviews.Add(bookReview);
            return _context.SaveChanges() == 1;
        }

        public List<BookReviewListItem> GetBookReviews()
        {
            List<BookReview> bookReviews = _context.BookReviews.ToList();
            List<BookReviewListItem> bookReviewListItems = bookReviews.Select(br => new BookReviewListItem()
            {
                ReviewId = br.ReviewId,
                ReviewTitle = br.ReviewTitle,
                ReviewText = br.ReviewText,
                CreatedReview = br.CreatedReview,
                BookRating = br.BookRating,
                Comments = br.Comments.Select(c => new CommentDisplayItem
                {
                    CommentId = c.CommentId,
                    CommentText = c.CommentText
                }).ToList(),
                BookId = br.BookId,
                Book = new BookDisplayItem
                {
                    Title = br.Book.Title,
                    Genre = br.Book.Genre,
                    Description = br.Book.Description,
                }
            }).ToList();

            return bookReviewListItems;
        }

        public BookReviewDetail GetReviewById(int Id)
        {
            BookReview reviewToGet = _context.BookReviews.Single(br => br.ReviewId == Id);
            BookReviewDetail bookReview = new BookReviewDetail
            {
                ReviewId = reviewToGet.ReviewId,
                ReviewText = reviewToGet.ReviewText,
                CreatedReview = reviewToGet.CreatedReview,
                ReviewTitle = reviewToGet.ReviewTitle,
                BookId = reviewToGet.BookId,
                BookRating = reviewToGet.BookRating,
                Book = new BookDisplayItem
                {
                    Title = reviewToGet.Book.Title,
                    Genre = reviewToGet.Book.Genre,
                    Description = reviewToGet.Book.Description,
                },
                Comments = reviewToGet.Comments.Select(c => )
            };

            return bookReview;
        }

        public bool UpdateBookReview(BookReviewEdit model)
        {
            BookReview reviewToEdit = _context.BookReviews.Single(br => br.ReviewId == model.ReviewId);
            reviewToEdit.ReviewText = model.ReviewText;
            reviewToEdit.ReviewTitle = model.ReviewTitle;
            reviewToEdit.BookRating = model.BookRating;
            return _context.SaveChanges() == 1;
        }

        public bool DeleteBookReview(int reviewId)
        {
            BookReview reviewToDelete = _context.BookReviews.Single(br => br.ReviewId == reviewId);
            _context.BookReviews.Remove(reviewToDelete);
            return _context.SaveChanges() == 1;
        }
    }
}
