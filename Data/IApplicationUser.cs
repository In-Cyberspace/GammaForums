using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IApplicationUser
    {
        /// <summary>
        /// Returns the application user corresponding to the Id input.
        /// </summary>
        ApplicationUser GetById(string Id);

        /// <summary>
        /// Returns a list of all application users.
        /// </summary>
        IEnumerable<ApplicationUser> GetAll();

        /// <summary>
        /// Set the application user's profile image.
        /// </summary>
        Task SetProfileImage(string Id, Uri uri);

        /// <summary>
        /// Increments the application user's rating.
        /// </summary>
        Task UpdateUserRating(string userId, Type type);
    }
}