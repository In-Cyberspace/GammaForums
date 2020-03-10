using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IPost
    {
        /// <summary>
        /// Fetch a post via its unique identifier.
        /// </summary>
        Post GetById(int Id);

        /// <summary>
        /// Collection all posts.
        /// </summary>
        IEnumerable<Post> GetAll();

        /// <summary>
        /// Collection of filtered posts using the forum and a search query.
        /// </summary>
        IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery);

        /// <summary>
        /// Collection of filtered posts.
        /// </summary>
        IEnumerable<Post> GetFilteredPosts(string searchQuery);

        /// <summary>
        /// Collection of posts via the forum's unique identifier.
        /// </summary>
        IEnumerable<Post> GetPostsByForum(int forumID);

        /// <summary>
        /// Collection of the latest posts.
        /// </summary>
        IEnumerable<Post> GetLatestPosts(int nPost);

        /// <summary>
        /// Task to add a new post.
        /// </summary>
        Task Add(Post post);

        /// <summary>
        /// Task to delete an existing post.
        /// </summary>
        Task Delete(int postId);

        /// <summary>
        /// Task to edit the content of a post.
        /// </summary>
        Task EditPostContent(int postId, string newContent);

        /// <summary>
        /// Task to add a reply to a post.
        /// </summary>
        Task AddReply(PostReply reply);
    }
}