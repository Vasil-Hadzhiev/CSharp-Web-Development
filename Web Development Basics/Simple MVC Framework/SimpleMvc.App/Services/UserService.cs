namespace SimpleMvc.App.Services
{
    using Contracts;
    using Data;
    using Models;
    using SimpleMvc.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class UserService : IUserService
    {
        public bool Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || 
                string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            using (var db = new NotesDbContext())
            {
                if (db.Users.FirstOrDefault(u => u.Username == username) == null)
                {
                    var user = new User
                    {
                        Username = username,
                        Password = password
                    };

                    db.Users.Add(user);
                    db.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public IEnumerable<UserViewModel> All()
        {
            using (var db = new NotesDbContext())
            {
                return db
                    .Users
                    .Select(u => new UserViewModel
                    {
                        Notes = u.Notes,
                        Username = u.Username,
                        Id = u.Id
                    })
                    .ToList();
            }
        }
        
        public UserViewModel GetUserById(int id)
        {
            using (var db = new NotesDbContext())
            {
                var user = db
                    .Users
                    .Where(u => u.Id == id)
                    .Select(u => new UserViewModel
                    {
                        Id = id,
                        Username = u.Username,
                        Notes = u.Notes
                    })
                    .FirstOrDefault();

                return user;
            }
        }
    }
}