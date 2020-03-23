using System;
using Microsoft.AspNetCore.Identity;

namespace Data
{
    /// <summary>
    /// Represents a registered user
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public int Rating { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime MemberSince { get; set; }
        public bool IsActive { get; set; }
    }
}
