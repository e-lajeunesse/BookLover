using BookLover.Data;
using BookLover.Models.BookModels;
using BookLover.Models.BookReviewModels;
using BookLover.Models.BookshelfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Services
{
    public class BookService
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private readonly Guid _userId;        
        private Random rand = new Random();

        public BookService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateBook(BookCreate model)
        {
            Book book = new Book()
            {
                Title = model.Title,
                Genre = model.Genre,
                Description = model.Description                
                //AuthorId = model.AuthorId
            };

            _context.Books.Add(book);
            return _context.SaveChanges() == 1;
        }

        public List<BookListItem> GetAllBooks()
        {
            List<Book> allBooks = _context.Books.ToList();
            return allBooks.Select(b => new BookListItem()
            {
                BookId = b.BookId,
                Title = b.Title,
                Genre = b.Genre,
                Description = b.Description,
                AverageRating = b.AverageRating
            }).ToList();
        }

        public List<BookListItem> GetBooksByGenre(string genre)
        {
            List<Book> allBooks = _context.Books.ToList();
            return allBooks.Where(b => b.Genre.ToLower() == genre.ToLower()).
                Select(b => new BookListItem()
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Genre = b.Genre,
                    Description = b.Description,
                    AverageRating = b.AverageRating
                }).ToList();
        }

        public List<BookListItem> SortBooksByRating()
        {
            List<Book> allBooks = _context.Books.ToList();
            List<Book> sortedBooks = allBooks.OrderByDescending(b => b.AverageRating).ToList();
            return sortedBooks.Select(b => new BookListItem()
            {
                BookId = b.BookId,
                Title = b.Title,
                Genre = b.Genre,
                Description = b.Description,
                AverageRating = b.AverageRating
            }).ToList();            
        }


        public BookDetail GetBookById(int id)
        {
            Book bookToGet = _context.Books.Single(b => b.BookId == id);

            BookDetail book = new BookDetail()
            {
                BookId = bookToGet.BookId,
                Title = bookToGet.Title,
                Genre = bookToGet.Genre,
                Description = bookToGet.Description,
                AverageRating = bookToGet.AverageRating,
                RecommendedBooks = GetRecommendedBooks(bookToGet)
            };
            return book;
        }

        public BookDetail GetBookByTitle(string title)
        {
            Book bookToGet = _context.Books.FirstOrDefault(b => b.Title.ToLower() == title.ToLower());
            if (bookToGet == default)
            {
                return default;
            }
            BookDetail book = new BookDetail
            {
                BookId = bookToGet.BookId,
                Title = bookToGet.Title,
                Genre = bookToGet.Genre,
                Description = bookToGet.Description,
                AverageRating = bookToGet.AverageRating,
                RecommendedBooks = GetRecommendedBooks(bookToGet)
            };
            return book;
        }

        //Method to get recommended books for GetBook by Title/Id methods
        public BookshelfDisplay GetRecommendedBooks(Book model)
        {
            Bookshelf recommendations = null;
            if (model.Bookshelves.Count > 0)
            {
                recommendations = model.Bookshelves[rand.Next(0, model.Bookshelves.Count)];
            }

            BookshelfDisplay recommendedBooks = (recommendations == null) ? null : new BookshelfDisplay
            {
                BookshelfId = recommendations.BookshelfId,
                Title = recommendations.Title,
                Books = recommendations.Books.Select(b => new BookshelfBookDisplay
                {
                    BookId = b.BookId,
                    Title = b.Title
                }).ToList()
            };

            return recommendedBooks;
        }

        public bool UpdateBook(BookEdit model)
        {
            Book bookToEdit = _context.Books.Single(b => b.BookId == model.BookId);
            bookToEdit.Title = model.Title;
            bookToEdit.Genre = model.Genre;
            bookToEdit.Description = model.Description;
            return _context.SaveChanges() == 1;
        }

        public bool DeleteBook(int bookId)
        {
            Book bookToDelete = _context.Books.Single(b => b.BookId == bookId);
            _context.Books.Remove(bookToDelete);
            return _context.SaveChanges() == 1;
        }

    }
}
