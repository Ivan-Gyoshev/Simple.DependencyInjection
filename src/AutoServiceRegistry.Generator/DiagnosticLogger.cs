using Microsoft.CodeAnalysis;

namespace AutoServiceRegistry.Generator;

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
        var descriptor = new DiagnosticDescriptor(
            id: "SG0002",
            title: "Source Generator Error",
            messageFormat: message,
            category: "SourceGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        var diagnostic = Diagnostic.Create(descriptor, Location.None);
        _context.ReportDiagnostic(diagnostic);
    }
}
