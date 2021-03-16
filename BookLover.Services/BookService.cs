using BookLover.Data;
using BookLover.Models;
using BookLover.Models.BookModels;
using BookLover.Models.BookReviewModels;
using BookLover.Models.BookshelfModels;
using BookLover.Models.GoogleBooksModels;
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
        private GoogleBooksAPIService service = new GoogleBooksAPIService();

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
                Description = model.Description,
                AuthorId = model.AuthorId,
            };

            _context.Books.Add(book);
            return _context.SaveChanges() == 1;
        }

        public bool AddBookByGoogleId(string id)
        {
            GoogleBook googleBook = service.GetBookByGoogleId(id).Result;
            Author author = _context.Authors.FirstOrDefault(a => a.FirstName == googleBook.AuthorFirstName 
                && a.LastName == googleBook.AuthorLastName);

            if (author == default)
            {
                throw new Exception($"Author with First name: {googleBook.AuthorFirstName}," +
                    $"and Last Name: {googleBook.AuthorLastName} not found," +
                    $" you must add author before book adding book."); 
            }

            Book bookToAdd = new Book
            {
                Title = googleBook.VolumeInfo.Title,
                Genre = googleBook.Genre,
                Description = googleBook.Description,
                AuthorId = author.AuthorId
            };
            _context.Books.Add(bookToAdd);
            return _context.SaveChanges() == 1;            
        }

        public bool AddBookByTitleAndAuthor(string title, string authorName)
        {
            GoogleBook googleBook = service.GetBookByTitleAndAuthor(title, authorName);
            Author author = _context.Authors.FirstOrDefault(a => a.FirstName == googleBook.AuthorFirstName
                && a.LastName == googleBook.AuthorLastName);

            if (author == default)
            {
                throw new Exception($"Author with First name: {googleBook.AuthorFirstName}," +
                    $"and Last Name: {googleBook.AuthorLastName} not found," +
                    $" you must add author before book adding book.");
            }

            Book bookToAdd = new Book
            {
                Title = googleBook.VolumeInfo.Title,
                Genre = googleBook.Genre,
                Description = googleBook.Description,
                AuthorId = author.AuthorId
            };
            _context.Books.Add(bookToAdd);
            return _context.SaveChanges() == 1;
        }

        public bool AddBooksByAuthor(string authorFirstName,string authorLastName)
        {
            Author author = AuthorIsAdded(authorFirstName, authorLastName);
            string authorFullName = authorFirstName + " " + authorLastName;
            List<GoogleBook> googleBooks = service.GetBooksByAuthor(authorFullName);

            foreach(GoogleBook book in googleBooks)
            {
                if (_context.Books.FirstOrDefault(b => b.Title == book.VolumeInfo.Title 
                    && b.AuthorId == author.AuthorId) != default)
                {
                    continue;
                }
                Book bookToAdd = new Book
                {
                    Title = book.VolumeInfo.Title,
                    Genre = book.Genre,
                    Description = book.Description,
                    AuthorId = author.AuthorId
                };
                _context.Books.Add(bookToAdd);
            }
            return _context.SaveChanges() > 0;
        }
        
        public Author AuthorIsAdded(string firstName, string lastName)
        {
            Author author = _context.Authors.FirstOrDefault(a => a.FirstName == firstName
                && a.LastName == lastName);
            if (author == default)
            {
                throw new Exception($"Author with First name: {firstName}," +
                    $"and Last Name: {lastName} not found," +
                    $" you must add author before book adding book.");
            }
            return author;
        }
        public List<BookListItem> GetAllBooks()
        {
            List<Book> books = _context.Books.ToList();
            List<BookListItem> bookListItems = books.Select(b => CreateBookListItem(b)).ToList();
            return bookListItems;
        }

        public List<BookListItem> GetBooksByGenre(string genre)
        {
            List<Book> allBooks = _context.Books.ToList();
            return allBooks.Where(b => b.Genre.ToLower().Contains(genre.ToLower())
                || genre.ToLower().Contains(b.Genre.ToLower())).
                Select(b => CreateBookListItem(b)).ToList();
        }

        public List<BookListItem> SortBooksByRating()
        {
            List<BookListItem> allBooks = _context.Books.Select(b => CreateBookListItem(b)).ToList();
            List<BookListItem> sortedBooks = allBooks.OrderByDescending(b => b.AverageRating).ToList();
            return sortedBooks;
        }

        public List<BookListItem> SortByGenreAndRating()
        {
            List<BookListItem> allBooks = _context.Books.Select(b => CreateBookListItem(b)).ToList();
            List<BookListItem> sortedBooks = allBooks.OrderBy(b => b.Genre.ToLower())
                .ThenByDescending(b => b.AverageRating).ToList();
            return sortedBooks;
        }

        public List<BookListItem> GetBooksByAuthorId(int id)
        {
            List<Book> matchingBooks = _context.Books.Where(b => b.AuthorId == id).ToList();
            return matchingBooks.Select(b => CreateBookListItem(b)).ToList();
        }

        public List<BookListItem> GetBooksByAuthorName(string firstName, string lastName)
        {
            List<Book> matchingBooks = _context.Books.Where(b =>
                b.Author.FirstName.ToLower() == firstName.ToLower()
                && b.Author.LastName.ToLower() == lastName.ToLower()).ToList();
            return matchingBooks.Select(b => CreateBookListItem(b)).ToList();
        }

        public BookDetail GetBookById(int id)
        {
            Book bookToGet = _context.Books.Single(b => b.BookId == id);
            return CreateBookDetail(bookToGet);
        }

        public BookDetail GetBookByTitle(string title)
        {
            Book bookToGet = _context.Books.FirstOrDefault(b => b.Title.ToLower().Contains(title));
            if (bookToGet == default)
            {
                return default;
            }
            return CreateBookDetail(bookToGet);
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

        //Helper Methods
        public BookListItem CreateBookListItem(Book model)
        {
            return new BookListItem()
            {

                BookId = model.BookId,
                Title = model.Title,
                Genre = model.Genre,
                Description = model.Description,
                AverageRating = model.BookReviews.Count > 0 ?
                    model.BookReviews.Select(r => r.BookRating).Sum() / model.BookReviews.Count : 0,                     
                ReviewCount = model.BookReviews.Count,
                AuthorId = model.AuthorId,
                Author = new AuthorDisplayItem
                {
                    FullName = model.Author.FirstName + " " + model.Author.LastName
                }
            };
        }

        public BookDetail CreateBookDetail(Book model)
        {
            BookDetail book = new BookDetail
            {
                BookId = model.BookId,
                Title = model.Title,
                Genre = model.Genre,
                Description = model.Description,
                AverageRating = model.BookReviews.Count > 0 ?
                    model.BookReviews.Select(r => r.BookRating).Sum() / model.BookReviews.Count : 0,
                ReviewCount = model.BookReviews.Count,
                RecommendedBooks = GetRecommendedBooks(model),
                BookReviews = model.BookReviews.Select(br => new BookReviewDisplayItem
                {
                    ReviewId = br.ReviewId,
                    ReviewText = br.ReviewText,
                    BookRating = br.BookRating,
                }).ToList(),
                AuthorId = model.AuthorId,
                Author = new AuthorDisplayItem
                {
                    FullName = model.Author.FirstName + " " + model.Author.LastName
                }
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
    }
}
