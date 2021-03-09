using BookLover.Data;
using BookLover.Models.BookshelfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Services
{
    public class BookshelfService
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private readonly Guid _userId;

        public BookshelfService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateBookshelf(BookshelfCreate model)
        {
            List<Book> allBooks = _context.Books.ToList();

            Bookshelf shelf = new Bookshelf()
            {
                Title = model.Title,
                BookIds = model.BookIds,
                Books = allBooks.Where(b => model.BookIds.Contains(b.BookId)).ToList()
            };

            _context.Bookshelves.Add(shelf);
            return _context.SaveChanges() >  0;
        }

        public List<BookshelfDisplay> GetAllBookShelves()
        {
            List<BookshelfDisplay> allBooks = _context.Bookshelves.Select(s => new BookshelfDisplay
            {
                BookshelfId = s.BookshelfId,
                Title = s.Title,

                Books = s.Books.Select(b => new BookshelfBookDisplay
                {
                    BookId = b.BookId,
                    Title = b.Title,
                }).ToList()
            }).ToList();

            return allBooks;
        }
    }
}
