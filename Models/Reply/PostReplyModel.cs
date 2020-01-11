namespace GammaForums.Models.Reply
{
    public class PostReplyModel : GammaForums.Base.Models.BasePostModel
    {
        public string ReplyContent { get; set; }
        public int PostId { get; set; }
    }
}