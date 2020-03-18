namespace GammaForums.Models.Forum
{
    /// <summary>
    /// The forum listing view model.
    /// </summary>
    public class ForumListingModel
    {
        /// <summary>
        /// The Forum's unique identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sets the title of the forum.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Sets the forum description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Sets the image url associated with the forum.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The number of posts associated with the forum.
        /// </summary>
        public int NumberOfPosts { get; set; }

        /// <summary>
        /// The number of members/users of the forum.
        /// </summary>
        public int NumberOfUsers { get; set; }

        /// <summary>
        /// Checks whether or not any recent post have been made on the forum.
        /// </summary>
        public bool HasRecentPost { get; set; }
    }
}
