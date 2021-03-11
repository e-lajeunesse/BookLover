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
                OwnerId = _userId,
                BookIds = model.BookIds,
                Books = allBooks.Where(b => model.BookIds.Contains(b.BookId)).ToList()
            };

            _context.Bookshelves.Add(shelf);
            return _context.SaveChanges() > 0;
        }

        public List<BookshelfDisplay> GetAllBookshelves()
        {
            List<BookshelfDisplay> allBookshelves = _context.Bookshelves.
                Select(s => new BookshelfDisplay
                {
                    BookshelfId = s.BookshelfId,
                    Title = s.Title,
                    Books = s.Books.Select(b => new BookshelfBookDisplay
                    {
                        BookId = b.BookId,
                        Title = b.Title,
                    }).ToList()
                }).ToList();

            return allBookshelves;
        }

        public List<BookshelfDisplay> GetAllBookShelvesByOwner()
        {
            List<BookshelfDisplay> allBookshelves = _context.Bookshelves.Where(s => s.OwnerId == _userId).
                Select(s => new BookshelfDisplay
                {
                    BookshelfId = s.BookshelfId,
                    Title = s.Title,

                    Books = s.Books.Select(b => new BookshelfBookDisplay
                    {
                        BookId = b.BookId,
                        Title = b.Title,
                    }).ToList()
                }).ToList();

            return allBookshelves;
        }

        public BookshelfDisplay GetBookshelfById(int id)
        {
            Bookshelf shelfToGet = _context.Bookshelves.Single(b => b.BookshelfId == id);
            BookshelfDisplay shelfDisplay = new BookshelfDisplay
            {
                BookshelfId = shelfToGet.BookshelfId,
                Title = shelfToGet.Title,
                Books = shelfToGet.Books.Select(b => new BookshelfBookDisplay
                {
                    BookId = b.BookId,
                    Title = b.Title
                }).ToList()
            };

            return shelfDisplay;

        }

        public bool UpdateBookshelf(BookshelfEdit model)
        {
            Bookshelf shelfToEdit = _context.Bookshelves.Single(b => b.BookshelfId == model.BookshelfId);
            shelfToEdit.Title = model.Title;
            shelfToEdit.BookIds = model.BookIds;
            shelfToEdit.Books = _context.Books.Where(b => model.BookIds.Contains(b.BookId)).ToList();

            // Removes books that should no longer be on shelf after update
            List<Book> booksToRemove = shelfToEdit.Books.Where(b => !model.BookIds.Contains(b.BookId)).ToList();
            foreach (Book book in booksToRemove)
            {
                shelfToEdit.Books.Remove(book);
            }

            return _context.SaveChanges() > 0;
        }

        public bool DeleteBookshelf(int id)
        {
            Bookshelf shelfToDelete = _context.Bookshelves.Single(b => b.BookshelfId == id);
            _context.Bookshelves.Remove(shelfToDelete);
            return _context.SaveChanges() == 1;
        }
    }
}
