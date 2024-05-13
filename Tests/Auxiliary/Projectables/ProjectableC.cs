using Builder4You.Interfaces;
using Tests.Auxiliary.Aggregates;
using Tests.Auxiliary.Buildeables;

namespace Tests.Auxiliary.Projectables
{
    internal class ProjectableC : IProjectable<AggregateC, ResourceC>
    {
        public ResourceC Project(AggregateC source)
        {
            throw new NotImplementedException();
        }
    }
}
