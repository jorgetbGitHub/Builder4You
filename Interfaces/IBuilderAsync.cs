using System.Linq.Expressions;

namespace Builder.Interfaces
{
    public interface IBuilderAsync<TResource> 
        where TResource : IBuildeable
    {
        IBuilderAsync<TResource> With<TProperty>(Expression<Func<TResource, TProperty>> property, TProperty value);
        IBuilderAsync<TResource> WithAsync<TProperty>(Expression<Func<TResource, TProperty>> property, Task<TProperty> value);
        Task<TResource> CreateAsync();

    }
}
