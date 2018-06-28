namespace MeTube.App.Services
{
    using Contracts;
    using Data;
    using MeTube.Models;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class TubeService : ITubeService
    {
        public bool Upload(string title, string author, string youtubeId, string description, int userId)
        {
            using (var db = new MeTubeContext())
            {
                var tubeExists = db
                    .Tubes
                    .FirstOrDefault(t => t.Title == title);

                if (tubeExists != null)
                {
                    return false;
                }

                var tube = new Tube
                {
                    Title = title,
                    Author = author,
                    Description = description,
                    YoutubeId = youtubeId,
                    UploaderId = userId
                };

                db.Tubes.Add(tube);
                db.SaveChanges();

                return true;
            }
        }

        public List<TubeHomeViewModel> All()
        {
            using (var db = new MeTubeContext())
            {
                var tubes = db
                    .Tubes
                    .Select(t => new TubeHomeViewModel
                    {
                        Title = t.Title,
                        Author = t.Author,
                        YouTubeId = t.YoutubeId
                    })
                    .ToList();

                return tubes;
            }
        }
    }
}