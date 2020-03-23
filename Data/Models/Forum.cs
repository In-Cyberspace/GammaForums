using System;
using System.Collections.Generic;

namespace Data
{
    /// <summary>
    /// Represents a forum created in the application
    /// </summary>
    public class Forum
    {
        /// <summary>
        /// The id associated with the forum
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the forum
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A short description of the forum
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The date and time that the forum was created
        /// </summary>
        public DateTime TimeCreated { get; set; }

        /// <summary>
        /// A url poiting to the location of the image
        /// associated with the forum
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// A collection of the posts made on the forum
        /// </summary>
        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
