using BookLover.Data;
using BookLover.Models.BookModels;
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


        //Need to Add
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
                Description = b.Description,
                AverageRating = b.AverageRating
            }).ToList();
        }


        
/*        public BookDetail GetBookById(int id)
        {
            Book bookToGet = _context.Books.Single(b => b.BookId == id);

            Bookshelf reccomendations = null;
            if (bookToGet.Bookshelves.Count > 0)
            {
                reccomendations = bookToGet.Bookshelves[rand.Next(0, bookToGet.Bookshelves.Count)];
            }

            BookDetail book = new BookDetail()
            {
                BookId = bookToGet.BookId,
                Title = bookToGet.Title,
                Description = bookToGet.Description,
                AverageRating = bookToGet.AverageRating,
                ReccomendedBooks = (reccomendations == null) ? null : new BookshelfDisplay
                {
                    BookshelfId = reccomendations.BookshelfId,
                    Title = reccomendations.Title,
                    Books = reccomendations.Books.Select(b => new BookshelfBookDisplayItem
                    {
                        BookId = b.BookId,
                        Title = b.Title
                    }).ToList()
                }

            };
            return book;
        }*/

    }
}
