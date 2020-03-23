using System.Collections.Generic;

namespace GammaForums.Models.ApplicationUser
{
    public class ProfileListModel
    {
        /// <summary>
        /// Collection of profile models
        /// </summary>
        public IEnumerable<ProfileModel> Profiles { get; set; }
    }
}