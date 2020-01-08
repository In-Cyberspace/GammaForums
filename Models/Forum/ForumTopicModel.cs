using System.Collections.Generic;
using GammaForums.Models.Post;

namespace GammaForums.Models.Forum
{
    public class ForumTopicModel
    {
        public ForumListingModel Forum { get; set; }
        public IEnumerable<PostListingModel> Posts { get; set; }
    }
}