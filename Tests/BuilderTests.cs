using Builder4You.Implementations;
using Tests.Auxiliary.Buildeables;

namespace Tests
{
    public class BuilderTests
    {
        private readonly Builder<ResourceA> _builderA;

        private BuilderTests()
        {
            _builderA = new();
        }

        [Fact]
        public void With_WhenAssignValueToExistingProperty_ThenReturnsIBuilder_Test()
        {
            // act
            
        }

        [Fact]
        public void With_WhenAssignValueToNotExistingProperty_ThenThrowsException_Test()
        {
            // act
        }

        [Fact]
        public void With_WhenAssignValueToNotSetteableProperty_ThenThrowsException_Test()
        {
            // act
        }

        [Fact]
        public void With_WhenAssignValueToField_ThenThrowsException_Test()
        {
            // act
        }

        [Fact]
        public void With_WhenAssignInvalidValue_ThenThrowsException_Test()
        {
            // act
        }

        [Fact]
        public void WithAsync_WhenAssignValueToExistingProperty_ThenReturnsIBuilderAsync_Test()
        {
            // act
        }

        [Fact]
        public void WithAsync_WhenAssignValueToNotExistingProperty_ThenThrowsException_Test()
        {
            // act
        }

        [Fact]
        public void WithAsync_WhenAssignValueToNotSetteableProperty_ThenThrowsException_Test()
        {
            // act
        }

        [Fact]
        public void WithAsync_WhenAssignValueToField_ThenThrowsException_Test()
        {
            // act
        }

        [Fact]
        public void WithAsync_WhenAssignInvalidValue_ThenThrowsException_Test()
        {
            // act
        }

        [Fact]
        public void Create_WhenNotHasPublicConstructor_ThenThrowsException_Test()
        {
            // act
        }

        [Fact]
        public void Create_WhenHasPublicConstructor_And_NoPropertyAssigned_ThenReturnsInstanceWithDefaultValues_Test()
        {
            // act
        }

        [Fact]
        public void Create_WhenHasPublicConstructor_And_PropertyAssigned_ThenReturnsInstanceWithValueSpecified_Test()
        {
            // act
        }

        [Fact]
        public void CreateAsync_WhenNotHasPublicConstructor_ThenThrowsException_Test()
        {
            // act
        }

        [Fact]
        public void CreateAsync_WhenHasPublicConstructor_And_NoPropertyAssigned_ThenReturnsInstanceWithDefaultValue_Test()
        {
            // act
        }

        [Fact]
        public void CreateAsync_WhenHasPublicConstructor_And_PropertyAssigned_Test()
        {
            // act
        }
    }
}
