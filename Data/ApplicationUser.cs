namespace Data
{
    /// <summary>
    /// Represents a user of the forum
    /// </summary>
    public class ApplicationUser
    {
        /// <summary>
        /// The unique identifier associated with the user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user's surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// The user's email
        /// </summary>
        public string Email { get; set; }
    }
}
