namespace KittenApp.Web.Services
{
    using Contracts;
    using KittenApp.Data;
    using KittenApp.Models;
    using SimpleMvc.Common;
    using System.Linq;

    public class UsersService : IUsersService
    {
        public User Create(string username, string email, string password)
        {
            using (var db = new KittenAppContext())
            {
                if (string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrWhiteSpace(email))
                {
                    return null;
                }

                User userExists = null;

                if (db.Users.Any())
                {
                    userExists = db
                        .Users
                        .AsQueryable()
                        .FirstOrDefault(u => u.Username == username || u.Email == email);
                }

                if (userExists != null)
                {
                    return null;
                }

                var user = new User
                {
                    Username = username,
                    Email = email,
                    PasswordHash = PasswordUtilities.GetPasswordHash(password),
                };

                db.Users.Add(user);
                db.SaveChanges();

                return user;
            }
        }

        public User Find(string username, string password)
        {
            using (var db = new KittenAppContext())
            {
                var user = db
                    .Users
                    .AsQueryable()
                    .FirstOrDefault(t => t.Username == username && t.PasswordHash == PasswordUtilities.GetPasswordHash(password));

                return user;
            }           
        }
    }
}