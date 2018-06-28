namespace MeTube.App.Services
{
    using Contracts;
    using Data;
    using Models;
    using MeTube.Models;
    using SimpleMvc.Common;
    using System.Collections.Generic;
    using System.Linq;

    public class UsersService : IUsersService
    {
        public User Create(string username, string password, string email)
        {
            using (var db = new MeTubeContext())
            {
                var userExists = db
                    .Users
                    .AsQueryable()
                    .FirstOrDefault(u => u.Username == username || u.Email == email);

                if (userExists != null)
                {
                    return null;
                }

                var user = new User
                {
                    Username = username,
                    Password = PasswordUtilities.GetPasswordHash(password),
                    Email = email
                };

                db.Users.Add(user);
                db.SaveChanges();

                return user;
            }
        }

        public User Find(string username, string password)
        {
            using (var db = new MeTubeContext())
            {
                var user = db
                    .Users
                    .AsQueryable()
                    .FirstOrDefault(u => u.Username == username && u.Password == PasswordUtilities.GetPasswordHash(password));

                return user;
            }
        }

        public List<UserProfileViewModel> UserTubes(int id)
        {
            using (var db = new MeTubeContext())
            {
                var tubes = db
                    .Tubes
                    .Where(t => t.UploaderId == id)
                    .Select(t => new UserProfileViewModel
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Author = t.Author
                    })
                    .ToList();

                return tubes;
            }
        }
    }
}