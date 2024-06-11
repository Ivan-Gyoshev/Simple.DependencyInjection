using Microsoft.CodeAnalysis;

namespace Simple.DI.Generator;

internal interface ILogger
{
    void LogError(string message);
}

internal sealed class DiagnosticLogger : ILogger
{
    private readonly SourceProductionContext _context;

    public DiagnosticLogger(SourceProductionContext context)
    {
        _context = context;
    }

    public void LogError(string message)
    {
        DiagnosticDescriptor descriptor = new DiagnosticDescriptor(
            id: "SG0002",
            title: "Source Generator Error",
            messageFormat: message,
            category: "AutoRegistrySourceGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        Diagnostic diagnostic = Diagnostic.Create(descriptor, Location.None);
        _context.ReportDiagnostic(diagnostic);
    }
}
