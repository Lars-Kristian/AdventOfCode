
using Microsoft.CodeAnalysis;

[Generator]
public class RunGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        Console.WriteLine($"Hello from {nameof(RunGenerator)}");
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        
    }
}