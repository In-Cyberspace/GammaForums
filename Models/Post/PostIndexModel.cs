using System.Collections.Generic;
using GammaForums.Models.Reply;

namespace GammaForums.Models.Post {
    public class PostIndexModel : GammaForums.Base.Models.BasePostModel {
        public string Title { get; set; }
        public string PostContent { get; set; }
        public IEnumerable<PostReplyModel> Replies { get; set; }
        public int ForumId { get; set; }
        public string ForumName { get; set; }

    }
}