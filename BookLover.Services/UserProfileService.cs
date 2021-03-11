using BookLover.Data;
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
            UserProfile userProfile = new UserProfile()
            {
                OwnerId = _userId,
                UserName = model.UserName
            };

            _context.UserProfiles.Add(userProfile);
            return _context.SaveChanges() > 0;
        }

        public List<UserProfileListItem> GetUserProfiles()
        {
            List<UserProfile> userProfiles = _context.UserProfiles.ToList();
            List<UserProfileListItem> userProfileListItem = userProfiles.Select(u => new UserProfileListItem()
            {
                UserProfileId = u.UserProfileId,
                UserName = u.UserName,
                BooksToRead = u.BooksToRead
            }).ToList();

            return userProfileListItem;
        }


    }
}