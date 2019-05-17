using System;
using System.Runtime.Serialization;
using Swashbuckle.AspNetCore.Filters;

namespace DotNetCoreSwap.Models
{
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

    public class UserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
