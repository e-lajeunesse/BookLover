using BookLover.Data;
using BookLover.Models.BookshelfModels;
using BookLover.Models.UserProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Services
{
    public class UserProfileService
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        private readonly Guid _userId;
        public UserProfileService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateUser(UserProfileCreate model)
        {
            List<Book> allBooks = _context.Books.ToList();

            UserProfile userProfile = new UserProfile()
            {
                OwnerId = _userId,
                UserName = model.UserName,
                BooksToRead = allBooks.Where(b => model.BookIds.Contains(b.BookId)).ToList()
            };

            _context.UserProfiles.Add(userProfile);
            return _context.SaveChanges() > 0;
        }

       /* public List<UserProfileListItem> GetUserProfiles()
        {
            List<UserProfile> userProfiles = _context.UserProfiles.ToList();
            List<UserProfileListItem> userProfileListItem = userProfiles.Select(u => new UserProfileListItem()
            {
                UserProfileId = u.UserProfileId,
                UserName = u.UserName,
                BooksToRead = u.BooksToRead
            }).ToList();

            return userProfileListItem;
        }*/

        public List<UserProfileDisplay> GetAllUserProfiles()
        {
            List<UserProfileDisplay> allprofiles = _context.UserProfiles.
                Select(upd => new UserProfileDisplay
                {
                    UserProfileId = upd.UserProfileId,
                    UserName = upd.UserName,
                    BooksToRead = upd.BooksToRead.Select(btr => new BookToReadDisplay
                    {
                        BookId = btr.BookId,
                        Title = btr.Title,
                    }).ToList(),
                    Bookshelves = upd.Bookshelves.Select(bs => new BookshelfDisplay
                    {
                        BookshelfId = bs.BookshelfId,
                        Title = bs.Title,
                        Books = bs.Books.Select(b => new BookshelfBookDisplay
                        {
                            BookId = b.BookId,
                            Title = b.Title
                        }).ToList()
                    }).ToList()
                }).ToList();

            return allprofiles;
        }

        public UserProfileDisplay GetUserById(int id)
        {
            UserProfile profileToGet = _context.UserProfiles.Single(u => u.UserProfileId == id);
            UserProfileDisplay profileDisplay = new UserProfileDisplay
            {
                UserProfileId = profileToGet.UserProfileId,
                UserName = profileToGet.UserName,
                BooksToRead = profileToGet.BooksToRead.Select(b => new BookToReadDisplay
                {
                    BookId = b.BookId,
                    Title = b.Title,
                }).ToList()
            };

            return profileDisplay;
        }



        public bool UpdateUserProfile(UserProfileEdit model)
        {
            UserProfile profileToEdit = _context.UserProfiles.Single(up => up.UserProfileId == model.UserProfileId);
            profileToEdit.UserName = model.UserName;
            profileToEdit.BooksToRead = _context.Books.Where(up => model.BookIds.Contains(up.BookId)).ToList();

            List<Book> booksToRemove = profileToEdit.BooksToRead.Where(up => !model.BookIds.Contains(up.BookId)).ToList();
            foreach (Book book in booksToRemove)
            {
                profileToEdit.BooksToRead.Remove(book);
            }

            return _context.SaveChanges() > 0;
        }

        public bool DeleteUserProfile(int id)
        {
            UserProfile profileToDelete = _context.UserProfiles.Single(up => up.UserProfileId == id);
            _context.UserProfiles.Remove(profileToDelete);
            return _context.SaveChanges() == 1;
        }
    }
}