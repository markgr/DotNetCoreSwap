using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreSwap.Models;
using System.Text;

namespace DotNetCoreSwap.Services
{
    public interface IUserService
    {
        Task<UserResponse> Authenticate(string username, string password);
        Task<IEnumerable<UserResponse>> GetAll();
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<UserResponse> _users = new List<UserResponse>
        {
            new UserResponse { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        public async Task<UserResponse> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            // return null if user not found
            if (user == null)
                return null;
            
			var bytes = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", user.Username, user.Password));
			var base64 = Convert.ToBase64String(bytes);
			user.Base64 = base64;

			// authentication successful so return user details without password
			user.Password = null;

			return user;
        }

        public async Task<IEnumerable<UserResponse>> GetAll()
        {
            // return users without passwords
            return await Task.Run(() => _users.Select(x => {
                x.Password = null;
                return x;
            }));
        }
    }
}
