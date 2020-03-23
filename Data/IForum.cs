using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// A contract to implement a set of forum service methods.
    /// </summary>
    public interface IForum
    {
        /// <summary>
        /// Return the forum associated with the unique identifier input.
        /// </summary>
        Forum GetById(int Id);

        /// <summary>
        /// Returns a collection of all forums.
        /// </summary>
        IEnumerable<Forum> GetAll();

        /// <summary>
        /// An operation to create a new forum.
        /// </summary>
        Task Create(Forum forum);

        /// <summary>
        /// An operation to delete an existing forum.
        /// </summary>
        Task Delete(int forumId);

        /// <summary>
        /// An operation to update the title of the forum.
        /// </summary>
        Task UpdateForumTitle(int forumId, string newTitle);

        /// <summary>
        /// An operation to update the forum description.
        /// </summary>
        Task UpdateForumDescription(int forumId, string newDescription);

        /// <summary>
        /// Returns a collection of all users who have created any posts or
        /// replies on the associated forum.
        /// </summary>
        IEnumerable<ApplicationUser> GetActiveUsers(int Id);

        /// <summary>
        /// Checks whether or not a forum has any recent posts.
        /// </summary>
        bool HasRecentPost(int id);
    }
}
