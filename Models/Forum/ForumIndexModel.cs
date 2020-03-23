using System.Collections.Generic;

namespace GammaForums.Models.Forum
{
    public class ForumIndexModel
    {
        public IEnumerable<ForumListingModel> ForumList { get; set; }
    }
}
