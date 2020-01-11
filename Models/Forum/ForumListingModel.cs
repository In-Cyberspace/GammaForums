namespace GammaForums.Models.Forum
{
    public class ForumListingModel
    {
        /// <summary>The Forum's unique identifire</summary>
        public int Id { get; set; }

        /// <summary>Forum title</summary>
        public string Title { get; set; }

        /// <summary>Forum description</summary>
        public string Description { get; set; }

        /// <summary>Image url associated with the forum</summary>
        public string ImageUrl { get; set; }
    }
}
