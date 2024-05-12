namespace Builder.Interfaces
{

    public interface IBuilderFrom<TResource>
        where TResource : IBuildeable
    {
        IBuilder<TResource> From<TSource>(TSource resource);
    }
}