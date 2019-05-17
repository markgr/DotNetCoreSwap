using System.Runtime.Serialization;

namespace DotNetCoreSwap.Models
{
    /// <summary>
    /// User response model
    /// </summary>
    [DataContract]
    public class UserResponse
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Username { get; set; }

        public string Password { get; set; }
        public string Base64 { set; get; }
    }

    /// <summary>
    /// User request model
    /// </summary>
    public class UserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
