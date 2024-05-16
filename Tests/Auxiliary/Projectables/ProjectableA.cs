using Builder4You.Interfaces;
using Tests.Auxiliary.Aggregates;
using Tests.Auxiliary.Buildeables;

namespace Tests.Auxiliary.Projectables
{
    internal class ProjectableA : IProjectable<AggregateA, ResourceA>
    {
        public ResourceA Project(AggregateA source)
        => new()
        {
            IntegerProperty = source.IntegerProperty,
            DecimalProperty = source.DecimalProperty,
            StringProperty = source.StringProperty,
            BooleanProperty = source.BooleanProperty,
        };
        
    }
}
