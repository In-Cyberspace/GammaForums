using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;

namespace GammaForums.Service
{
    public class ApplicationUserService : IApplicationUser
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.ApplicationUsers;
        }

        public ApplicationUser GetById(string Id)
        {
            return GetAll().FirstOrDefault(user => user.Id == Id);
        }

        public Task IncrementRating(string Id, Type type)
        {
            throw new NotImplementedException();
        }

        public async Task SetProfileImage(string Id, Uri uri)
        {
            ApplicationUser user = GetById(Id);
            user.ProfileImageUrl = uri.AbsoluteUri;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}