using Builder4You.Interfaces;

namespace Tests.Auxiliary.Buildeables
{
    internal class ResourceA : IBuildeable
    {
        public int IntegerProperty { get; set; }
        public decimal DecimalProperty { get; set; }
        public string? StringProperty { get; set; }
        public bool BooleanProperty { get; set; }
    }
}
