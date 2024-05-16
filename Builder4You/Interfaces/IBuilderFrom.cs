namespace Builder4You.Interfaces
{

    public interface IBuilderFrom<TResource>
        where TResource : IBuildeable, new()
    {
        IBuilder<TResource> From<TSource>(TSource source);
        IBuilderAsync<TResource> FromAsync<TSource>(Task<TSource> sourceTask);
    }
}