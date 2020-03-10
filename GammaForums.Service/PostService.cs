using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;

namespace GammaForums.Service
{
    public class PostService : IPost
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task AddReply(PostReply reply)
        {
            _context.PostReplies.Add(reply);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int postId)
        {
            throw new NotImplementedException();
        }

        public Task EditPostContent(int postId, string newContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts
             .Include(post => post.User)
             .Include(post => post.Replies)
             .ThenInclude(reply => reply.User)
             .Include(post => post.Forum);
        }

        public Post GetById(int postId)
        {
            return _context.Posts
                .Where(post => post.Id == postId)
                .Include(post => post.User)
                .Include(post => post.Replies)
                .ThenInclude(reply => reply.User)
                .Include(post => post.Forum)
                .First();
        }

        public IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery)
            ? forum.Posts
            : forum.Posts
            .Where(post
                => post.Title.Contains(searchQuery)
                || post.Content.Contains(searchQuery));
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            return GetAll().Where(post
                => post.Title.Contains(searchQuery)
                || post.Content.Contains(searchQuery));
        }

        public IEnumerable<Post> GetLatestPosts(int nPost)
        {
            return GetAll()
            .OrderByDescending(post => post.TimeCreated)
            .Take(nPost);
        }

        public IEnumerable<Post> GetPostsByForum(int forumID)
        {
            return _context
                .Forums
                .Where(forum => forum.Id == forumID)
                .First()
                .Posts;
        }
    }
}
