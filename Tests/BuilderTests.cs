using Builder4You.Exceptions;
using Builder4You.Implementations;
using Builder4You.Interfaces;
using FluentAssertions;
using Tests.Auxiliary.Buildeables;

namespace Tests
{
    public class BuilderTests
    {
        private readonly Builder<ResourceA> _builderA;

        public BuilderTests()
        {
            _builderA = new();
        }

        [Fact]
        public void With_WhenAssignValueToExistingProperty_ThenReturnsIBuilder_Test()
        {
            // act
            var builder = _builderA.With(r => r.BooleanProperty, true);

            // assert
            builder.Should().BeOfType<Builder<ResourceA>>()
                .And.BeAssignableTo<IBuilder<ResourceA>>();
        }

        [Fact]
        public void With_WhenAssignValueToNotExistingProperty_ThenThrowsRequiredMemberAccessException_Test()
        {
            // act & assert
            Assert.Throws<RequiredMemberAccessException>(() => _builderA.With(r => "not-existing-property", "not-valid"));
        }

        [Fact]
        public void With_WhenAssignValueToNotSetteableProperty_ThenThrowsPropertyNotSetteableException_Test()
        {
            // act & assert
            Assert.Throws<PropertyNotSetteableException>(() => _builderA.With(r => r.NotSettableProperty, "not-valid"));
        }

        [Fact]
        public void With_WhenAssignValueToPublicField_ThenReturnsIBuilder_Test()
        {
            // act
            var builder = _builderA.With(r => r.StringField, "test-resource");

            // assert
            builder.Should().BeOfType<Builder<ResourceA>>()
                .And.BeAssignableTo<IBuilder<ResourceA>>();
        }

        [Fact]
        public void WithAsync_WhenAssignValueToExistingProperty_ThenReturnsIBuilderAsync_Test()
        {
            // act
            var builderAsync = _builderA.WithAsync(r => r.BooleanProperty, Task.FromResult(true));

            // assert
            builderAsync.Should().BeOfType<Builder<ResourceA>>()
                .And.BeAssignableTo<IBuilderAsync<ResourceA>>();
        }

        [Fact]
        public void WithAsync_WhenAssignValueToNotExistingProperty_ThenReturnsIBuilderAsync_Test()
        {
            // act
            var builderAsync = _builderA.WithAsync(r => "not-existing-property", Task.FromResult("not-valid"));

            // assert
            builderAsync.Should().BeOfType<Builder<ResourceA>>()
                .And.BeAssignableTo<IBuilderAsync<ResourceA>>();
        }

        [Fact]
        public void WithAsync_WhenAssignValueToNotSetteableProperty_ThenReturnsBuilderAsync_Test()
        {
            // act
            var builderAsync = _builderA.WithAsync(r => r.NotSettableProperty, Task.FromResult("not-valid"));

            // assert
            builderAsync.Should().BeOfType<Builder<ResourceA>>()
                .And.BeAssignableTo<IBuilderAsync<ResourceA>>();
        }

        [Fact]
        public void WithAsync_WhenAssignValueToPublicField_ThenReturnsBuilderAsync_Test()
        {
            // act
            var builderAsync = _builderA.WithAsync(r => r.StringField, Task.FromResult("test-resource"));

            // assert
            builderAsync.Should().BeOfType<Builder<ResourceA>>()
                .And.BeAssignableTo<IBuilderAsync<ResourceA>>();
        }

        [Fact]
        public void Create_WhenHasParameterlessPublicConstructor_And_NoPropertyAssigned_ThenReturnsInstanceWithDefaultValues_Test()
        {
            // act
            ResourceA resourceA = _builderA.Create();

            // assert
            resourceA.Should().BeEquivalentTo(new ResourceA());
        }

        [Fact]
        public void Create_WhenHasParameterlessPublicConstructor_And_PropertyAssigned_ThenReturnsInstanceWithValueSpecified_Test()
        {
            // act
            ResourceA resourceA =
                _builderA
                    .With(r => r.BooleanProperty, true)
                    .With(r => r.StringProperty, "test-resource")
                    .With(r => r.DecimalProperty, 3.1416m)
                    .Create();

            // assert
            resourceA.Should().BeEquivalentTo(new ResourceA
            {
                BooleanProperty = true,
                StringProperty = "test-resource",
                DecimalProperty = 3.1416m
            });
        }

        [Fact]
        public async Task CreateAsync_WhenHasParameterlessPublicConstructor_And_NoPropertyAssigned_ThenReturnsInstanceWithDefaultValue_Test()
        {
            // arrange
            var builderAAsync =
                _builderA
                    .WithAsync(r => r.StringProperty, Task.FromResult<string?>(null));
             
            // act
            var resourceA = await _builderA.CreateAsync();

            // assert
            resourceA.Should().BeEquivalentTo(new ResourceA());

        }

        [Fact]
        public async Task CreateAsync_WhenHasParameterlessPublicConstructor_And_PropertyAssigned_Test()
        {
            // arrange
            var builderAAsync =
                _builderA
                    .WithAsync(r => r.StringProperty, Task.FromResult<string?>("test-resource"));

            // act
            var resourceA = await _builderA.CreateAsync();

            // assert
            resourceA.Should().BeEquivalentTo(new ResourceA()
            {
                StringProperty = "test-resource"
            });
        }
    }
}
