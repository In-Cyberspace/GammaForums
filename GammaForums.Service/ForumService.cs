using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;

namespace GammaForums.Service
{
    /// <summary>
    /// A set of forum service methods.
    /// </summary>
    public class ForumService : IForum
    {
        protected readonly ApplicationDbContext _context;

        public ForumService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Forum forum)
        {
            _context.Add(forum);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int forumId)
        {
            Forum forum = GetById(forumId);
            _context.Remove(forum);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ApplicationUser> GetActiveUsers(int Id)
        {
            IEnumerable<Post> posts = GetById(Id).Posts;

            if (posts != null || !posts.Any())
            {
                return posts.Select(p => p.User)
                .Union(
                    posts.SelectMany(p => p.Replies).Select(r => r.User)
                ).Distinct();
            }

            return new List<ApplicationUser>();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.Forums.Include(forum => forum.Posts);
        }

        public Forum GetById(int Id)
        {
            return _context
            .Forums
            .Where(f => f.Id == Id)
            .Include(f => f.Posts)
            .ThenInclude(p => p.User)
            .Include(f => f.Posts)
            .ThenInclude(p => p.Replies)
            .ThenInclude(r => r.User)
            .FirstOrDefault();
        }

        public bool HasRecentPost(int id)
        {
            const int hoursAgo = 12;
            DateTime window = DateTime.Now.AddHours(-hoursAgo);
            return GetById(id).Posts.Any(post => post.TimeCreated > window);
        }

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
