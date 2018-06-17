namespace SimpleMvc.App.Services
{
    using Contracts;
    using Data;
    using SimpleMvc.Models;
    using System.Linq;

    public class NoteService : INoteService
    {
        public void Add(int userId, string title, string content)
        {
            using (var db = new NotesDbContext())
            {
                var user = db
                    .Users
                    .FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    var note = new Note
                    {
                        Title = title,
                        Content = content,
                        UserId = userId
                    };

                    db.Notes.Add(note);
                    db.SaveChanges();
                }
            }
        }
    }
}