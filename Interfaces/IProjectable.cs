namespace Builder.Interfaces
{
    public interface IProjectable<TSource, TResource>
        where TResource : IBuildeable
    {
        TResource Project(TSource source);
    }
}
