using System.Linq.Expressions;

namespace Builder.Interfaces
{
    public interface IBuilder<TResource> // BuilderSync
        where TResource : IBuildeable
    {
        IBuilder<TResource> With<TProperty>(Expression<Func<TResource, TProperty>> property, TProperty value);
        IBuilderAsync<TResource> WithAsync<TProperty>(Expression<Func<TResource, TProperty>> property, Task<TProperty> value);
        TResource Create();
    }
}
