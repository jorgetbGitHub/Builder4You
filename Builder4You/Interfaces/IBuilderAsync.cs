using System.Linq.Expressions;

namespace Builder4You.Interfaces
{
    public interface IBuilderAsync<TResource>
        where TResource : IBuildeable, new()
    {
        IBuilderAsync<TResource> With<TProperty>(Expression<Func<TResource, TProperty>> property, TProperty value);
        IBuilderAsync<TResource> WithAsync<TProperty>(Expression<Func<TResource, TProperty>> property, Task<TProperty> valueTask);
        Task<TResource> CreateAsync();

    }
}
