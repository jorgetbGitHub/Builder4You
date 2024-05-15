using Builder4You.Extensions;
using Builder4You.Implementations;
using Builder4You.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Tests.Auxiliary.Aggregates;
using Tests.Auxiliary.Buildeables;
using Tests.Auxiliary.Projectables;

namespace Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddProjectablesTest()
        {
            // arrange
            var serviceCollection = new ServiceCollection();

            // act
            serviceCollection.AddProjectables();

            // assert
            var provider = serviceCollection.BuildServiceProvider();
            var projectableA = provider.GetService<IProjectable<AggregateA, ResourceA>>();
            var projectableB = provider.GetService<IProjectable<AggregateB, ResourceB>>();
            var projectableC = provider.GetService<IProjectable<AggregateC, ResourceC>>();

            Assert.NotNull(projectableA);
            Assert.IsType<ProjectableA>(projectableA);

            Assert.NotNull(projectableB);
            Assert.IsType<ProjectableB>(projectableB);

            Assert.NotNull(projectableC);
            Assert.IsType<ProjectableC>(projectableC);

        }

        [Fact]
        public void AddBuildersTest()
        {
            // arrange
            var serviceCollection = new ServiceCollection();

            // act
            serviceCollection.AddBuilders();

            // assert
            var provider = serviceCollection.BuildServiceProvider();

            var builderA = provider.GetService<IBuilder<ResourceA>>();
            var builderFromA = provider.GetService<IBuilderFrom<ResourceA>>();

            builderA.Should().NotBeNull().And.BeOfType<Builder<ResourceA>>();
            builderFromA.Should().NotBeNull().And.BeOfType<BuilderFrom<ResourceA>>();

            var builderC = provider.GetService<IBuilder<ResourceC>>();
            var builderFromC = provider.GetService<IBuilderFrom<ResourceC>>();

            builderC.Should().NotBeNull().And.BeOfType<Builder<ResourceC>>();
            builderFromC.Should().NotBeNull().And.BeOfType<BuilderFrom<ResourceC>>();

        }

    }
}