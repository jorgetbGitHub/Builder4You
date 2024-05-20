using Builder4You.Exceptions;
using Builder4You.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Builder4You.Implementations
{
    public class BuilderFrom<TResource>(IServiceProvider provider) : Builder<TResource>, IBuilderFrom<TResource>
        where TResource : IBuildeable, new()
    {
        private readonly IServiceProvider _provider = provider;
        public IBuilder<TResource> From<TSource>(TSource source)
        {
            var projectable = _provider.GetService<IProjectable<TSource, TResource>>();
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
            var projectable = _provider.GetService<IProjectable<TSource, TResource>>();
            if (projectable is not null)
            {
                Tasks.Add(sourceTask.ContinueWith(completed =>
                {
                    Resource = projectable.Project(completed.Result);
                }));
            }
            else
            {
                throw new ProjectableNotExistsException(typeof(TSource), typeof(TResource));
            }

            return this;
        }
    }
}
