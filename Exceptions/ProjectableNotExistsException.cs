namespace Builder4You.Exceptions
{
    public class ProjectableNotExistsException : Exception
    {
        public ProjectableNotExistsException(Type sourceType, Type resourceType)
            : base($"")
        {
        }
    }
}
