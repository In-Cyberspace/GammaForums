using GammaForums.Models.Forum;

namespace GammaForums.Models.Post
{
    public class PostListingModel : GammaForums.Base.Models.BasePostModel
    {
        public string Title { get; set; }
        public string DatePosted { get; set; }
        public ForumListingModel Forum { get; set; }
        public int RepliesCount { get; set; }
    }
}