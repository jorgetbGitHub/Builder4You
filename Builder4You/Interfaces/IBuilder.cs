using System.Linq.Expressions;

namespace Builder4You.Interfaces
{
    public interface IBuilder<TResource>
        where TResource : IBuildeable, new()
    {
        IBuilder<TResource> With<TProperty>(Expression<Func<TResource, TProperty>> property, TProperty value);
        IBuilderAsync<TResource> WithAsync<TProperty>(Expression<Func<TResource, TProperty>> property, Task<TProperty> value);
        TResource Create();
    }
}
