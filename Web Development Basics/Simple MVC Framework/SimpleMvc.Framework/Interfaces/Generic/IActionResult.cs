namespace SimpleMvc.Framework.Interfaces.Generic
{
    public interface IActionResult<TModel> : IInvocable
    {
        IRenderable<TModel> Action { get; set; }
    }
}