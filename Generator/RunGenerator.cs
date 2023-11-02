using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

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