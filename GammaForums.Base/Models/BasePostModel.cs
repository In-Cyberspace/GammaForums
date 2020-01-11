using System;

namespace GammaForums.GammaForums.Base.Models
{
    public class BasePostModel
    {
        public int Id { get; set; }
        public string AuthorID { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorImageUrl { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}