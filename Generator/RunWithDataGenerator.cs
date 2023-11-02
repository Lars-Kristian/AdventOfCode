using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Generator
{
    //Code is created with the help of Andrew Lock's blog https://andrewlock.net/creating-a-source-generator-part-1-creating-an-incremental-source-generator/
    
    [Generator]
    public class RunWithDataGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            // Add the marker attribute to the compilation
            context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
                "RunWithDataAttribute.g.cs",
                SourceText.From(RunWithDataGeneratorSource.Attribute, Encoding.UTF8)));

            // Do a simple filter for enums
            var enumDeclarations = context.SyntaxProvider
                .CreateSyntaxProvider<MethodDeclarationSyntax>(
                    predicate: static (s, _) => IsSyntaxTargetForGeneration(s), // select enums with attributes
                    transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)) // sect the enum with the [EnumExtensions] attribute
                .Where(static m => m is not null)!; // filter out attributed enums that we don't care about

            // Combine the selected enums with the `Compilation`
            IncrementalValueProvider<(Compilation, ImmutableArray<MethodDeclarationSyntax>)> compilationAndEnums
                = context.CompilationProvider.Combine(enumDeclarations.Collect());

            // Generate the source using the compilation and enums
            context.RegisterSourceOutput(compilationAndEnums,
                static (spc, source) => Execute(source.Item1, source.Item2, spc));
        }
        
        static bool IsSyntaxTargetForGeneration(SyntaxNode node)
            => node is MethodDeclarationSyntax m && m.AttributeLists.Count > 0;
        
        static MethodDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
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
                    if (fullName == "Generator.RunWithDataAttribute")
                    {
                        return methodDeclarationSyntax;
                    }
                }
            }
            
            return null;
        }   
        
        static void Execute(Compilation compilation, ImmutableArray<MethodDeclarationSyntax> methods, SourceProductionContext context)
        {
            if (methods.IsDefaultOrEmpty)
            {
                // nothing to do yet
                return;
            }

            // I'm not sure if this is actually necessary, but `[LoggerMessage]` does it, so seems like a good idea!
            var methodsEnums = methods.Distinct();

            // Convert each EnumDeclarationSyntax to an EnumToGenerate
            List<EnumToGenerate> methodsToGenerate = GetTypesToGenerate(compilation, methodsEnums, context.CancellationToken);

            // If there were errors in the EnumDeclarationSyntax, we won't create an
            // EnumToGenerate for it, so make sure we have something to generate
            if (enumsToGenerate.Count > 0)
            {
                // generate the source code and add it to the output
                string result = SourceGenerationHelper.GenerateExtensionClass(enumsToGenerate);
                context.AddSource("EnumExtensions.g.cs", SourceText.From(result, Encoding.UTF8));
            }
        }
    }

    public static class RunWithDataGeneratorSource
    {
        public const string Attribute = @"
namespace Generator
{
    [AttributeUsage(System.AttributeTargets.Method)]
    public class RunWithDataAttribute : Attribute
    {
        public string Path { get; }

        public RunWithDataAttribute(string path)
        {
            Path = path;
        }
    }
}";
    }
}