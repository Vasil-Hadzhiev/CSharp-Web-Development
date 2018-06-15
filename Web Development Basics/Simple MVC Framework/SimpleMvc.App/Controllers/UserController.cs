namespace SimpleMvc.App.Controllers
{
    using Framework.Attributes.Methods;
    using Framework.Controllers;
    using Framework.Interfaces;
    using Framework.Interfaces.Generic;
    using Models;
    using Services;
    using Services.Interfaces;
    using System.Collections.Generic;

    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly INoteService noteService;

        public UserController()
        {
            this.userService = new UserService();
            this.noteService = new NoteService();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserViewModel model)
        {
            var username = model.Username;
            var password = model.Password;

            var success = this.userService.Register(username, password);

            return View();
        }

        [HttpGet]
        public IActionResult<IEnumerable<UserViewModel>> All()
        {
            var users = this.userService.All();

            return View(users);
        }

        [HttpGet]
        public IActionResult<UserViewModel> Profile(int id)
        {
            var user = this.userService.GetUserById(id);

            return View(user);
        }

        [HttpPost]
        public IActionResult<UserViewModel> Profile(NoteViewModel model)
        {
            var userId = model.UserId;
            var title = model.Title;
            var content = model.Content;

            this.noteService.Add(userId, title, content);

            return this.Profile(userId);
        }
    }
}