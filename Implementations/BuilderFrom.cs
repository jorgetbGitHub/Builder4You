using Builder.Exceptions;
using Builder.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Builder.Implementations
{
    public class BuilderFrom<TResource>(IServiceProvider provider) : Builder<TResource>, IBuilderFrom<TResource> 
        where TResource : IBuildeable
    {
        private readonly IServiceProvider _provider = provider;
        public IBuilder<TResource> From<TSource>(TSource source)
        {
            var projectable = _provider.GetRequiredService<IProjectable<TSource, TResource>>();
            if (projectable is not null)
            {
                Resource = projectable.Project(source);
            }
            else
            {
                throw new ProjectableNotExistsException(typeof(TSource), typeof(TResource));
            }

            return this;
        }

        public IBuilderAsync<TResource> FromAsync<TSource>(Task<TSource> sourceTask)
        {
            var projectable = _provider.GetRequiredService<IProjectable<TSource, TResource>>();
            if (projectable is not null)
            {
                sourceTask.ContinueWith(completed =>
                {
                    Resource = projectable.Project(completed.Result);
                });
            }
            else
            {
                throw new ProjectableNotExistsException(typeof(TSource), typeof(TResource));
            }

            return this;
        }
    }
}
