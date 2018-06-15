namespace SimpleMvc.App.Services.Interfaces
{
    public interface INoteService
    {
        void Add(int userId, string title, string content);
    }
}