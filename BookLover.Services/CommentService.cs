using BookLover.Data;
using BookLover.Models.BookReviewModels;
using BookLover.Models.CommentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Services
{
    public class CommentService
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private readonly Guid _userId;
        public CommentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateComment(CommentCreate model)
        {
            Comment comment = new Comment()
            {
                OwnerId = _userId,
                CommentText = model.CommentText,
                CreatedUtc = DateTimeOffset.Now,
                ReviewId = model.ReviewId
            };
            _context.Comments.Add(comment);
            return _context.SaveChanges() == 1;
        }

        public List<CommentListItem> GetAllComments()
        {
            List<CommentListItem> allComments = _context.Comments.Select(c => new CommentListItem
            {
                CommentId = c.CommentId,
                CommentText = c.CommentText,
                CreatedUtc = c.CreatedUtc,
                Review = new BookReviewDisplayItem
                {
                    ReviewId = c.BookReview.ReviewId,
                    BookRating = c.BookReview.BookRating,
                    ReviewText = c.BookReview.ReviewText
                }
            }).ToList();
            return allComments;
        }

        public bool EditComment(CommentEdit model)
        {
            Comment commentToEdit = _context.Comments.Single(c => c.CommentId == model.CommentId && _userId == c.OwnerId);
            commentToEdit.CommentText = model.CommentText;
            commentToEdit.ReviewId = model.ReviewId;
            return _context.SaveChanges() == 1;
        }

        public bool DeleteComment(int id)
        {

            Comment commentToDelete = _context.Comments.Single(c => c.CommentId == id && c.OwnerId == _userId);
            _context.Comments.Remove(commentToDelete);

            return _context.SaveChanges() == 1;
        }
    }
}
