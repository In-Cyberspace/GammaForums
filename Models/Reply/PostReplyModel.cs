namespace GammaForums.Models.Reply
{
    public class PostReplyModel : GammaForums.Base.Models.BasePostModel
    {
        /// <summary>
        // The content of the post reply.
        /// </summary>
        public string ReplyContent { get; set; }
        
        /// <summary>
        /// The unique identifier for the parent post.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// The title of the parent post.
        /// </summary>
        public string PostTitle { get; set; }

        /// <summary>
        /// The contents of the parent post.
        /// </summary>
        public string PostContent { get; set; }

        /// <summary>
        /// The unique identifier for the parent forum.
        /// </summary>
        public int ForumId { get; set; }

        /// <summary>
        /// The title of the parent forum.
        /// </summary>
        public string ForumTitle { get; set; }

        /// <summary>
        /// The image url for the parent forum.
        /// </summary>
        public string ForumImageUrl { get; set; }       
    }
}