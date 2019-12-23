using System;
using System.Collections.Generic;

namespace GammaForums.Data.Models
{
    /// <summary>
    /// Represents a post made on a forum
    /// </summary>
    public class Post
    {
        /// <summary>
        /// The Id associated with the post
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the post
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The content of the post
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The date and time that the pdot was created
        /// </summary>
        public DateTime TimeCreated { get; set; }

        /// <summary>
        /// Represents the user that created the post
        /// </summary>
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// Represents the forum that the psot was made on
        /// </summary>
        public virtual Forum Forum { get; set; }

        /// <summary>
        /// A collection of the replies made to the post
        /// </summary>
        public virtual IEnumerable<PostReply> Replies { get; set; }
    }
}
