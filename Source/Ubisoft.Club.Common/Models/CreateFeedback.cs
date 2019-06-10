using System;
using System.ComponentModel.DataAnnotations;
using LiteDB;

namespace Ubisoft.Club.Common.Models
{
    /// <summary>
    /// Feedback.
    /// </summary>
    public class CreateFeedback
    {
        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        /// <value>The session identifier.</value>
        [Required]
        public Guid SessionId { get; set; }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>The rate.</value>
        [Range(1, 5)]
        public int Rate { get; set; }
    }
}
