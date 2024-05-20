using Builder4You.Implementations;
using Builder4You.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Builder4You.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static (Type Class, Type Interface) BuilderType = (typeof(Builder<>), typeof(IBuilder<>));
        private static (Type Class, Type Interface) BuilderFromType = (typeof(BuilderFrom<>), typeof(IBuilderFrom<>));
        public static IServiceCollection AddBuilders(this IServiceCollection services)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var buildeableClasses = assembly.GetTypes()
                        .Where(t => IsBuildeableClass(t) && HasPublicParameterlessConstructor(t));

                    foreach (Type buildeableClass in buildeableClasses)
                    {
                        services.AddScoped(
                            BuilderType.Interface.MakeGenericType(buildeableClass),
                            BuilderType.Class.MakeGenericType(buildeableClass));

                        services.AddScoped(
                            BuilderFromType.Interface.MakeGenericType(buildeableClass),
                            BuilderFromType.Class.MakeGenericType(buildeableClass));
                    }
                }
                catch 
                { 
                    // Not loaded assembly is Ignored.
                    // It's a problem related to race condition applied at testing context.
                }
            }

            return services;
        }
        public static IServiceCollection AddProjectables(this IServiceCollection services)
        {
            var projectableType = typeof(IProjectable<,>);

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var projectableClasses = assembly.GetTypes()
                        .Where(t => IsProjectableClass(t) && HasPublicParameterlessConstructor(t));

                    foreach (Type projectableClass in projectableClasses)
                    {
                        foreach (var projectableInterface in projectableClass.GetInterfaces()
                            .Where(t => t.Name == projectableType.Name))
                        {
                            services.AddScoped(projectableInterface, projectableClass);
                        }
                    }
                }
                catch
                {
                    // Not loaded assembly is Ignored.
                    // It's a problem related to race condition applied at testing context.
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

        private static bool HasPublicParameterlessConstructor(Type t) 
            => t.GetConstructor(Type.EmptyTypes) is not null;

        private static bool IsBuildeableClass(Type t)
        {
            if (!t.IsClass || t.IsAbstract) return false;

            var buildeableType = typeof(IBuildeable);

            return t.GetInterfaces().Any(
                i => i.Name == buildeableType.Name);
        }
    }
}
