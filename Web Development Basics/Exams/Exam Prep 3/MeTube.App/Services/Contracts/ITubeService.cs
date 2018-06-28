namespace MeTube.App.Services.Contracts
{
    using MeTube.Models;
    using Models;
    using System.Collections.Generic;

    public interface ITubeService
    {
        bool Upload(string title, string author, string youtubeId, string description, int userId);

        List<TubeHomeViewModel> All();
    }
}