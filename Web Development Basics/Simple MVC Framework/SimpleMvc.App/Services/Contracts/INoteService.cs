namespace SimpleMvc.App.Services.Contracts
{
    public interface INoteService
    {
        void Add(int userId, string title, string content);
    }
}