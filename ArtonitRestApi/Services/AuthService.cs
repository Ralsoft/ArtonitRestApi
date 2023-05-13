using ArtonitRestApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ArtonitRestApi.Services
{
    public class AuthService
    {
        private List<User> _users = new List<User>()
        {
            new User { Id = 1, Username = "user1", Password = "password1" },
            new User { Id = 2, Username = "user2", Password = "password2" }
        };

        public User Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);

            return user;
        }
    }
}
