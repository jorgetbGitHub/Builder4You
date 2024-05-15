namespace Builder4You.Exceptions
{
    public class FieldNotPublicException(string fieldName) 
        : Exception($"Property ${fieldName} requires to have public access level")
    {
    }
}
