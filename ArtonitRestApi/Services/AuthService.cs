using ArtonitRestApi.Models;

namespace ArtonitRestApi.Services
{
    public class AuthService
    {
        public User Login(string username, string password)
        {
            var query = $@"select p.id_pep, p.flag, p.id_orgctrl from people p 
                where p.login='{username}' and p.pswd='{password}' and p.""ACTIVE"">0";

            var user = DatabaseService.Get<User>(query);

            return user;
        }
    }
}
