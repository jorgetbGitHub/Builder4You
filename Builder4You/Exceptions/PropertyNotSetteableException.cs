namespace Builder4You.Exceptions
{
    public class PropertyNotSetteableException(string propertyName) 
        : Exception($"Property ${propertyName} requires to have a set accessor")
    {
    }
}
