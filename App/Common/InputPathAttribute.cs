[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class InputPathAttribute : Attribute
{
    public string Path { get; }

    public InputPathAttribute(string path)
    {
        Path = path;
    }
}