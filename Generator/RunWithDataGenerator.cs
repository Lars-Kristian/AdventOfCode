using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

//Code is created with the help of Andrew Lock's blog https://andrewlock.net/creating-a-source-generator-part-1-creating-an-incremental-source-generator/

[Generator]
public class RunWithDataGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        Console.WriteLine("Hello from RunWithDataGenerator");
        // Add the marker attribute to the compilation
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            "RunWithDataAttribute.g.cs",
            SourceText.From(RunWithDataGeneratorSource.Attribute, Encoding.UTF8)));

        // Do a simple filter for enums
        var methodDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider<MethodDeclarationSyntax>(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s), // select enums with attributes
                transform: static (ctx, _) =>
                    GetSemanticTargetForGeneration(ctx)) // sect the enum with the [EnumExtensions] attribute
            .Where(static m => m is not null)!; // filter out attributed enums that we don't care about

        // Combine the selected enums with the `Compilation`
        IncrementalValueProvider<(Compilation, ImmutableArray<MethodDeclarationSyntax>)> compilationAndEnums
            = context.CompilationProvider.Combine(methodDeclarations.Collect());


        // Generate the source using the compilation and enums
        context.RegisterSourceOutput(compilationAndEnums,
            static (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    private static bool IsSyntaxTargetForGeneration(SyntaxNode node)
    {
        return node is MethodDeclarationSyntax m && m.AttributeLists.Count > 0;
    }

    private static MethodDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        // we know the node is a EnumDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        var methodDeclarationSyntax = (MethodDeclarationSyntax)context.Node;

        // loop through all the attributes on the method
        foreach (AttributeListSyntax attributeListSyntax in methodDeclarationSyntax.AttributeLists)
        {
            foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
            {
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                {
                    // weird, we couldn't get the symbol, ignore it
                    continue;
                }

                var fullName = attributeSymbol.ContainingType.ToDisplayString();

                // Is the attribute the [EnumExtensions] attribute?
                if (fullName == "RunWithDataAttribute")
                {
                    return methodDeclarationSyntax;
                }
            }
        }

        return null;
    }


    static void Execute(Compilation compilation, ImmutableArray<MethodDeclarationSyntax> methods,
        SourceProductionContext context)
    {
        if (methods.IsDefaultOrEmpty) return;

        // I'm not sure if this is actually necessary, but `[LoggerMessage]` does it, so seems like a good idea!
        var methodsEnums = methods.Distinct();

        // Convert each EnumDeclarationSyntax to an EnumToGenerate
        var methodToGenerates = GetTypesToGenerate(compilation, methodsEnums, context.CancellationToken);

        // If there were errors in the EnumDeclarationSyntax, we won't create an
        // EnumToGenerate for it, so make sure we have something to generate
        
        if (methodToGenerates.Count > 0)
        {
            // generate the source code and add it to the output
            string result = GenerateRunMethod(methodToGenerates);
            context.AddSource("GeneratedRunMethods.g.cs", SourceText.From(result, Encoding.UTF8));
        }
    }

    static List<MethodToGenerate> GetTypesToGenerate(Compilation compilation,
        IEnumerable<MethodDeclarationSyntax> methods, CancellationToken ct)
    {
        // Create a list to hold our output
        var methodToGenerateList = new List<MethodToGenerate>();
        // Get the semantic representation of our marker attribute 
        INamedTypeSymbol? enumAttribute = compilation.GetTypeByMetadataName("RunWithDataAttribute");

        if (enumAttribute == null) return methodToGenerateList;

        foreach (var methodDeclarationSyntax in methods)
        {
            // stop if we're asked to
            ct.ThrowIfCancellationRequested();

            // Get the semantic representation of the enum syntax
            var semanticModel = compilation.GetSemanticModel(methodDeclarationSyntax.SyntaxTree);
            var declaredSymbol = semanticModel.GetDeclaredSymbol(methodDeclarationSyntax);
            if (declaredSymbol is not IMethodSymbol typeSymbol)
            {
                continue;
            }

            var attributeArgument = GetAttributeConstructorArgument(enumAttribute, typeSymbol.GetAttributes());
            methodToGenerateList.Add(new MethodToGenerate(
                typeSymbol.ContainingType.ToString(), 
                typeSymbol.Name, 
                attributeArgument));

            // Get the full type name of the enum e.g. Colour, 
            // or OuterClass<T>.Colour if it was nested in a generic type (for example)
            //var symbolName = typeSymbol.ToString();


            // Get all the members in the enum
            /*
            var enumMembers = typeSymbol.GetMembers();
            var members = new List<string>(enumMembers.Length);

            // Get all the fields from the enum, and add their name to the list
            foreach (ISymbol member in enumMembers)
            {
                if (member is IFieldSymbol field && field.ConstantValue is not null)
                {
                    members.Add(member.Name);
                }
            }
            */
            // Create an EnumToGenerate for use in the generation phase
        }

        return methodToGenerateList;
    }

    public class MethodToGenerate
    {
        public MethodToGenerate(string methodNamespace, string methodName, string? attributeArgument)
        {
            MethodNamespace = methodNamespace;
            MethodName = methodName;
            AttributeArgument = attributeArgument;
        }

        public string MethodNamespace { get; set; }

        public string MethodName { get; set; }

        public string? AttributeArgument { get; set; }
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

                // make sure we don't have any errors
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

            /*
            // now check for named arguments
            if (!attributeData.NamedArguments.IsEmpty)
            {
                foreach (KeyValuePair<string, TypedConstant> arg in attributeData.NamedArguments)
                {
                    TypedConstant typedConstant = arg.Value;
                    if (typedConstant.Kind == TypedConstantKind.Error)
                    {
                        // have an error, so don't try and do any generation
                        return;
                    }
                    else
                    {
                        // Use the constructor argument or property name to infer which value is set
                        switch (arg.Key)
                        {
                            case "extensionClassName":
                                className = (string)typedConstant.Value;
                                break;
                            case "ExtensionNamespaceName":
                                namespaceName = (string)typedConstant.Value;
                                break;
                        }
                    }
                }
            }
            */

            break;
        }

        return result;
    }

    private static string GenerateRunMethod(List<MethodToGenerate> methodToGenerates)
    {
        var sb = new StringBuilder();
        sb.Append(@"using System.Diagnostics;

public class GeneratedRunMethods
{");
        foreach (var methodToGenerate in methodToGenerates)
        {
            sb.Append(@"
    public static void GeneratedRunFor"); sb.Append(methodToGenerate.MethodName); sb.Append(@"()
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

public static class RunWithDataGeneratorSource
{
    public const string Attribute = @"
[AttributeUsage(System.AttributeTargets.Method)]
public class RunWithDataAttribute : Attribute
{
    public string Path { get; }

    public RunWithDataAttribute(string path)
    {
        Path = path;
    }
}";
}