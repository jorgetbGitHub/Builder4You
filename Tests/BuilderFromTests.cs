using Builder4You.Implementations;
using Builder4You.Interfaces;
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

        public BuilderFromTests()
        {
            _serviceProvider = Substitute.For<IServiceProvider>();
            _serviceProvider.GetService<IProjectable<AggregateA, ResourceA>>()
                .Returns(new ProjectableA());
            _builderFromA = new(_serviceProvider);
        }

        [Fact]
        public void From_WhenProjectableIsRegistered_ThenReturnsIBuilder_Test() // ensure Projectable.Project is called 1 time
        {
            // arrange
            // act
            // assert
        }

        [Fact]
        public void From_WhenProjectableIsNotRegistered_ThenThrowsProjectableNotExistsException_Test()
        {
            // arrange
            // act
            // assert
        }

        [Fact]
        public void FromAsync_WhenProjectableIsRegistered_ThenReturnsIBuilderAsync_Test() // ensure Projectable.Project is called 1 time
        {
            // arrange
            // act
            // assert
        }

        [Fact]
        public void FromAsync_WhenProjectableIsNotResgistered_ThenThrowsProjectableNotExistsException_Test()
        {
            // arrange
            // act
            // assert
        }
    }
}
