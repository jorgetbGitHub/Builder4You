using Builder4You.Implementations;
using Builder4You.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Builder4You.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static (Type Class, Type Interface) BuilderType = (typeof(Builder<>), typeof(IBuilder<>));
        private static (Type Class, Type Interface) BuilderFromType = (typeof(BuilderFrom<>), typeof(IBuilderFrom<>));
        public static IServiceCollection AddBuilders(this IServiceCollection services)
        {
            var buildeableType = typeof(IBuildeable);

            var buildeableClasses = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(IsBuildeableClass);

            foreach (var buildeableClass in buildeableClasses)
            {
                services.AddScoped(
                    BuilderType.Interface.MakeGenericType(buildeableClass),
                    BuilderType.Class.MakeGenericType(buildeableClass));

                services.AddScoped(
                    BuilderFromType.Interface.MakeGenericType(buildeableClass),
                    BuilderFromType.Class.MakeGenericType(buildeableClass));
            }

            return services;
        }
        public static IServiceCollection AddProjectables(this IServiceCollection services)
        {
            var projectableType = typeof(IProjectable<,>);

            var projectableClasses = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(IsProjectableClass);

            foreach (var projectableClass in projectableClasses)
            {
                foreach (var projectableInterface in projectableClass.GetInterfaces()
                    .Where(t => t.Name == projectableType.Name))
                {
                    services.AddScoped(projectableInterface, projectableClass);
                }
            }

            return services;
        }

        private static bool IsProjectableClass(Type t)
        {
            if (!t.IsClass || t.IsAbstract) return false;

            var projectableType = typeof(IProjectable<,>);

            return t.GetInterfaces().Any(
                i => i.Name == projectableType.Name);
        }

        private static bool IsBuildeableClass(Type t)
        {
            if (!t.IsClass || t.IsAbstract) return false;

            var buildeableType = typeof(IBuildeable);

            return t.GetInterfaces().Any(
                i => i.Name == buildeableType.Name);
        }
    }
}
