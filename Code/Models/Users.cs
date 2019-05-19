using System.Runtime.Serialization;

namespace DotNetCoreSwap.Models
{
    /// <summary>
    /// User response model
    /// </summary>
    [DataContract]
    public class UserResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        [DataMember]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the base64.
        /// </summary>
        /// <value>The base64.</value>
        public string Base64 { set; get; }
    }

    /// <summary>
    /// User request model
    /// </summary>
    public class UserRequest
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }
    }
}
