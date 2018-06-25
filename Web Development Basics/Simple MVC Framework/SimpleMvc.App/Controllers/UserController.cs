namespace SimpleMvc.App.Controllers
{
    using Data;
    using Framework.ActionResults;
    using Framework.Attributes.Methods;
    using Framework.Contracts;
    using Framework.Controllers;
    using Models;
    using SimpleMvc.Models;
    using System.Linq;

    public class UserController : Controller
    {
        private NotesDbContext db;

        public UserController()
        {
            this.db = new NotesDbContext();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserViewModel model)
        {
            using (this.db)
            {
                if (this.db.Users.Any(u => u.Username == model.Username))
                {
                    return new RedirectResult("/users/register");
                }

                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password
                };


                this.db.Users.Add(user);
                this.db.SaveChanges();
            }

            return this.View();
        }
    }
}