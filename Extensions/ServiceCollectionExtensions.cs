using Builder.Implementations;
using Builder.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Builder.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBuilders(this IServiceCollection services)
        {
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
    }
}
