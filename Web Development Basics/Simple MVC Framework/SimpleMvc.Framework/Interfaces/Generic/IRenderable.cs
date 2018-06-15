namespace SimpleMvc.Framework.Interfaces.Generic
{
    public interface IRenderable<TModel> : IRenderable
    {
        TModel Model { get; set; }
    }
}