using Builder4You.Interfaces;

namespace Tests.Auxiliary.Buildeables
{
    internal class ResourceB : IBuildeable
    {
        public string? StringProperty { get; set; }
        private ResourceB() { }
    }
}
