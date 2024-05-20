using AutoFixture;
using Builder4You.Exceptions;
using Builder4You.Implementations;
using Builder4You.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tests.Auxiliary.Aggregates;
using Tests.Auxiliary.Buildeables;
using Tests.Auxiliary.Projectables;

namespace Tests
{
    public class BuilderFromTests
    {
        private readonly BuilderFrom<ResourceA> _builderFromA;
        private readonly IServiceProvider _serviceProvider;

        private readonly Fixture _fixture;

        public BuilderFromTests()
        {
            _serviceProvider = Substitute.For<IServiceProvider>();
            _builderFromA = new(_serviceProvider);

            _fixture = new Fixture();
        }

        [Fact]
        public void From_WhenProjectableIsRegistered_ThenReturnsIBuilder_Test() // ensure Projectable.Project is called 1 time
        {
            // arrange
            var aggregateA = _fixture.Create<AggregateA>();
            _serviceProvider.GetService<IProjectable<AggregateA, ResourceA>>()
                .Returns(new ProjectableA());

            // act
            var builder = _builderFromA.From(aggregateA);

            // assert
            builder.Should().BeOfType<BuilderFrom<ResourceA>>()
                .And.BeAssignableTo<IBuilder<ResourceA>>();
        }

        [Fact]
        public void From_WhenProjectableIsRegisteredAndCreate_ThenResourceShouldBeEquivalentToAggregate_Test()
        {
            // arrange
            var aggregateA = _fixture.Create<AggregateA>();
            _serviceProvider.GetService<IProjectable<AggregateA, ResourceA>>()
                .Returns(new ProjectableA());

            // act
            var resourceA = _builderFromA
                .From(aggregateA)
                .Create();

            // arrange
            resourceA.Should().BeEquivalentTo(aggregateA);
        }

        [Fact]
        public void From_WhenProjectableIsNotRegistered_ThenThrowsProjectableNotExistsException_Test()
        {
            // arrange
            var aggregateA = _fixture.Create<AggregateA>();
            _serviceProvider.GetService<IProjectable<AggregateA, ResourceA>>()
                .Returns(null as ProjectableA);

            // act & assert
            Assert.Throws<ProjectableNotExistsException>(() => _builderFromA.From(aggregateA));
        }

        [Fact]
        public void FromAsync_WhenProjectableIsRegistered_ThenReturnsIBuilderAsync_Test()
        {
            // arrange
            var aggregateA = _fixture.Create<AggregateA>();
            _serviceProvider.GetService<IProjectable<AggregateA, ResourceA>>()
                .Returns(new ProjectableA());

            // act
            var builderAsync = _builderFromA.FromAsync(Task.FromResult(aggregateA));

            // assert
            builderAsync.Should().BeOfType<BuilderFrom<ResourceA>>()
                .And.BeAssignableTo<IBuilderAsync<ResourceA>>();
        }

        [Fact]
        public async Task FromAsync_WhenProjectableIsRegisteredAndCreateAsync_ThenResourceShouldBeEquivalentToAggregate_Test()
        {
            // arrange
            var aggregateA = _fixture.Create<AggregateA>();
            _serviceProvider.GetService<IProjectable<AggregateA, ResourceA>>()
                .Returns(new ProjectableA());

            // act
            var resourceA = await _builderFromA
                .FromAsync(Task.FromResult(aggregateA))
                .CreateAsync();

            // assert
            resourceA.Should().BeEquivalentTo(aggregateA);
        }

        [Fact]
        public void FromAsync_WhenProjectableIsNotResgistered_ThenThrowsProjectableNotExistsException_Test()
        {
            // arrange
            var aggregateA = _fixture.Create<AggregateA>();
            _serviceProvider.GetService<IProjectable<AggregateA, ResourceA>>()
                .Returns(null as ProjectableA);

            // act & assert
            Assert.Throws<ProjectableNotExistsException>(() => _builderFromA.FromAsync(Task.FromResult(aggregateA)));    
        }
    }
}
