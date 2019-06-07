using System;
using System.ComponentModel.DataAnnotations;
using LiteDB;

namespace Ubisoft.Club.Common.Models
{
    /// <summary>
    /// Feedback.
    /// </summary>
    public class Feedback
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public ObjectId Id { get; set; }

        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        /// <value>The session identifier.</value>
        [Required]
        public Guid SessionId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>The rate.</value>
        [Range(1, 5)]
        public int Rate { get; set; }
    }
}
