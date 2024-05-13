namespace Builder4You.Interfaces
{
    public interface IProjectable<TSource, TResource>
        where TResource : IBuildeable
    {
        TResource Project(TSource source);
    }
}
