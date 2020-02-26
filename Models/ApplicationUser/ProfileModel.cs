using System;
using Microsoft.AspNetCore.Http;

namespace GammaForum.Models.ApplicationUser
{
    public class ProfileModel
    {
        /// <summary>
        /// The application user's unique identifier
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The application user's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The application user's username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The application user's rating
        /// </summary>
        public string UserRating { get; set; }

        /// <summary>
        /// The url path to the application user's profile image
        /// </summary>
        public string ProfileImageUrl { get; set; }

        /// <summary>
        /// Captures the date and time for when the application user registered
        /// </summary>
        public DateTime MemberSince { get; set; }

        /// <summary>
        /// Handles image uploads by the application user
        /// </summary>
        public IFormFile ImageUpload { get; set; }
    }
}