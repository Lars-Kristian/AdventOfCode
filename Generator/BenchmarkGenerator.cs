using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Generator;

[Generator]
public class BenchmarkGenerator : IIncrementalGenerator
{
    private static string Namespace = "BenchmarkGenerator";
    private static string ClassName = "GeneratedBenchmarks";
    private static string AttributeName = "GenerateBenchmarkAttribute";
    private static string NamespaceAndAttributeName = $"{Namespace}.{AttributeName}";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        CreateMarkAttribute(context);

        var methodDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider<MethodDeclarationSyntax>(
                predicate: static (syntaxNode, _) => FilterSyntaxTarget(syntaxNode),
                transform: static (syntaxContext, _) => FilterSemanticTarget(syntaxContext))
            .Where(static x => x is not null);

        var incrementalValueProvider = context.CompilationProvider.Combine(methodDeclarations.Collect());

        context.RegisterSourceOutput(incrementalValueProvider,
            static (sourceProductionContext, source) => Execute(source.Left, source.Right, sourceProductionContext));
    }

    private static void CreateMarkAttribute(
        IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            $"{AttributeName}.g.cs",
            SourceText.From(BenchmarkSourceUtil.GetAttributeSource(Namespace, AttributeName), Encoding.UTF8)));
    }

    private static bool FilterSyntaxTarget(SyntaxNode syntaxNode)
    {
        return syntaxNode is MethodDeclarationSyntax m && m.AttributeLists.Count > 0;
    }

    private static MethodDeclarationSyntax? FilterSemanticTarget(GeneratorSyntaxContext syntaxContext)
    {
        var methodDeclarationSyntax = (MethodDeclarationSyntax)syntaxContext.Node;

        foreach (var attributeListSyntax in methodDeclarationSyntax.AttributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                if (syntaxContext.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol
                    attributeSymbol) continue;
                if (attributeSymbol.ContainingType.ToDisplayString() == NamespaceAndAttributeName)
                    return methodDeclarationSyntax;
            }
        }

        return null;
    }

    static void Execute(Compilation compilation,
        ImmutableArray<MethodDeclarationSyntax> methods,
        SourceProductionContext context)
    {
        if (methods.IsDefaultOrEmpty) return;

        var methodToGenerates = GetTypesToGenerate(compilation, methods, context.CancellationToken);

        if (methodToGenerates.Count < 0) return;

        string result = BenchmarkSourceUtil.GetSource(methodToGenerates, Namespace, ClassName);
        context.AddSource($"{ClassName}.g.cs", SourceText.From(result, Encoding.UTF8));
    }

    static List<MethodToGenerate> GetTypesToGenerate(Compilation compilation,
        IEnumerable<MethodDeclarationSyntax> methods, CancellationToken ct)
    {
        var result = new List<MethodToGenerate>();
        
        var enumAttribute = compilation.GetTypeByMetadataName(NamespaceAndAttributeName);

        if (enumAttribute == null) return result;

        foreach (var methodDeclarationSyntax in methods)
        {
            ct.ThrowIfCancellationRequested();

            var semanticModel = compilation.GetSemanticModel(methodDeclarationSyntax.SyntaxTree);
            var declaredSymbol = semanticModel.GetDeclaredSymbol(methodDeclarationSyntax);
            if (declaredSymbol is not IMethodSymbol typeSymbol)
            {
                continue;
            }

            var attributeArgument = GetAttributeConstructorArgument(enumAttribute, typeSymbol.GetAttributes());

            var methodToGenerate = new MethodToGenerate(
                typeSymbol.ContainingType.ToString(),
                typeSymbol.ContainingType.Name,
                typeSymbol.Name,
                attributeArgument);
            
            result.Add(methodToGenerate);
        }

        return result;
    }

    private static string? GetAttributeConstructorArgument(INamedTypeSymbol namedTypeSymbol,
        ImmutableArray<AttributeData> attributeDataList)
    {
        var result = string.Empty;

        foreach (var attributeData in attributeDataList)
        {
            if (!namedTypeSymbol.Equals(attributeData.AttributeClass, SymbolEqualityComparer.Default)) continue;
            
            if (!attributeData.ConstructorArguments.IsEmpty)
            {
                var args = attributeData.ConstructorArguments;

                foreach (var arg in args)
                {
                    if (arg.Kind == TypedConstantKind.Error) return string.Empty;
                }

                switch (args.Length)
                {
                    case 1:
                        return (string)args[0].Value;
                }
            }

            //Add code for named arguments
            throw new Exception("Unable to find ConstructorArguments. Implementation for NamedArguments is missing.");
        }

        return result;
    }
}

public class MethodToGenerate
{
    public MethodToGenerate(string methodNamespace, string methodParent, string methodName,
        string attributeArgument)
    {
        MethodNamespace = methodNamespace;
        MethodParent = methodParent;
        MethodName = methodName;
        AttributeArgument = attributeArgument;
    }

    public string MethodParent { get; set; }

    public string MethodNamespace { get; set; }

    public string MethodName { get; set; }

    public string? AttributeArgument { get; set; }
}

public static class BenchmarkSourceUtil
{
    public static string GetAttributeSource(string nameSpace, string attributeName)
    {
        var sb = new StringBuilder();
        sb.Append(@"namespace ");
        sb.Append(nameSpace);
        sb.Append(@"
{
    [AttributeUsage(System.AttributeTargets.Method)]
    public class ");
        sb.Append(attributeName);
        sb.Append(@" : Attribute
    {
        public string Path { get; }

        public ");
        sb.Append(attributeName);
        sb.Append(@"(string path)
        {
            Path = path;
        }
    }
}");
        return sb.ToString();
    }

    public static string GetSource(List<MethodToGenerate> methodToGenerates, string nameSpace, string className)
    {
        var sb = new StringBuilder();
        sb.Append(@"using System.Diagnostics;

namespace "); sb.Append(nameSpace); sb.Append(@";

public class "); sb.Append(className); sb.Append(@"
{");
        foreach (var methodToGenerate in methodToGenerates)
        {
            var debug = methodToGenerate.MethodParent;
            sb.Append(@"
    public static void "); sb.Append(methodToGenerate.MethodParent); sb.Append(methodToGenerate.MethodName); sb.Append(@"()
    {
        var sw = Stopwatch.StartNew();
        var text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), """);
            sb.Append(methodToGenerate.AttributeArgument);
            sb.Append(@"""));");

            sb.Append(@"
        Console.WriteLine(");
            sb.Append(methodToGenerate.MethodNamespace);
            sb.Append(".");
            sb.Append(methodToGenerate.MethodName);
            sb.Append(@"(text));");
            sb.Append(@"
        sw.Stop();
        Console.WriteLine($""Time used {sw.ElapsedMilliseconds}ms"");
    }
");
        } 
        sb.Append(@"
}");
        return sb.ToString();
    }
}