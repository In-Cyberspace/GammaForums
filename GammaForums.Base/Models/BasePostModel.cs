using System;

namespace GammaForums.GammaForums.Base.Models
{
    public class BasePostModel
    {
        /// <summary>
        /// The unique identifier for the post.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// The post author's unique Identifier.
        /// </summary>
        public string AuthorId { get; set; }
        
        /// <summary>
        /// The post author's name.
        /// </summary>
        public string AuthorName { get; set; }
        
        /// <summary>
        /// The post author's rating.
        /// </summary>
        public int AuthorRating { get; set; }
        
        /// <summary>
        /// The post author's image url.
        /// </summary>
        public string AuthorImageUrl { get; set; }
        
        /// <summary>
        /// Date and time of creation for the post.
        /// </summary>
        public DateTime TimeCreated { get; set; }
        
        /// <summary>
        /// Checks whether or not the post author has the admin role.
        /// </summary>
        public bool IsAuthorAdmin { get; set; }
    }
}