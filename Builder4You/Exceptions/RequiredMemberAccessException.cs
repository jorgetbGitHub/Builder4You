using System.Linq.Expressions;

namespace Builder4You.Exceptions
{
    public class RequiredMemberAccessException(Expression expression) 
        : Exception($"Expected lambda function to access property/field but found {expression} instead")
    {
    }
}
