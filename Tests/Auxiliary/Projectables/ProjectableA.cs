using Builder4You.Interfaces;
using Tests.Auxiliary.Aggregates;
using Tests.Auxiliary.Buildeables;

namespace Tests.Auxiliary.Projectables
{
    internal class ProjectableA : IProjectable<AggregateA, ResourceA>
    {
        public ResourceA Project(AggregateA source)
        {
            throw new NotImplementedException();
        }
    }
}
