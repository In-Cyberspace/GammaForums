using System;

namespace Data
{
    /// <summary>
    /// Represents a reply made to a post on a forum
    /// </summary>
    public class PostReply
    {
        /// <summary>
        /// The id associated with the post reply
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// THe content of the post reply
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The date and time the post reply was made
        /// </summary>
        public DateTime TimeCreated { get; set; }

        /// <summary>
        /// The user who made the post reply
        /// </summary>
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// The post to which the post reply was made to
        /// </summary>
        public virtual Post Post { get; set; }
    }
}
