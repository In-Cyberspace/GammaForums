using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IPost
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int forumID);
        IEnumerable<Post> GetLatestPosts(int nPost);

        Task Add(Post post);
        Task Delete(int postId);
        Task EditPostContent(int postId, string newContent);
        Task AddReply(PostReply reply);
    }
}