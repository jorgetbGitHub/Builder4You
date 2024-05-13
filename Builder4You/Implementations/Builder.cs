using Builder4You.Interfaces;
using System.Linq.Expressions;

namespace Builder4You.Implementations
{
    public class Builder<TResource> : IBuilder<TResource>, IBuilderAsync<TResource>
        where TResource : IBuildeable
    {
        protected TResource? Resource;
        protected List<Task> Tasks = [];
        public TResource Create()
        {
            throw new NotImplementedException();
        }

        public Task<TResource> CreateAsync()
        {
            return Task.WhenAll(Tasks).ContinueWith(allCompleted => Resource);
        }

        public IBuilder<TResource> With<TProperty>(Expression<Func<TResource, TProperty>> property, TProperty value)
        {
            throw new NotImplementedException();
        }

        public IBuilderAsync<TResource> WithAsync<TProperty>(Expression<Func<TResource, TProperty>> property, Task<TProperty> value)
        {
            throw new NotImplementedException();
        }

        IBuilderAsync<TResource> IBuilderAsync<TResource>.With<TProperty>(Expression<Func<TResource, TProperty>> property, TProperty value)
        {
            throw new NotImplementedException();
        }
    }
}
