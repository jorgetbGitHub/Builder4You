using Builder4You.Interfaces;
using Tests.Auxiliary.Aggregates;
using Tests.Auxiliary.Buildeables;

namespace Tests.Auxiliary.Projectables
{
    internal class ProjectableB : IProjectable<AggregateB, ResourceB>
    {
        public ResourceB Project(AggregateB source)
        {
            throw new NotImplementedException();
        }
    }
}
