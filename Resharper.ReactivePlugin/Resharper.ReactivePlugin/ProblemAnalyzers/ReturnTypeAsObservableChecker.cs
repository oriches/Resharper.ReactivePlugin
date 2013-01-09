namespace Resharper.ReactivePlugin.ProblemAnalyzers
{
    using Highlighters;
    using JetBrains.ReSharper.Daemon;
    using JetBrains.ReSharper.Daemon.Stages;
    using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.Tree;
    using Helpers;

    [ElementProblemAnalyzer(new[] { typeof(IReturnStatement) }, HighlightingTypes = new[] { typeof(AsObservableHighlighting) })]
    public sealed class ReturnTypeAsObservableChecker : ElementProblemAnalyzer<IReturnStatement>
    {
        protected override void Run(IReturnStatement returnStatement, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            if (!ReturnStatementHelper.IsContainingDeclarationReturnTypeIObservable(returnStatement))
            {
                return;
            }

            if (!ReturnStatementHelper.IsContainingDeclarationPublic(returnStatement))
            {
                return;
            }

            if (ReturnStatementHelper.IsReturnTypeAsObservable(returnStatement))
            {
                return;
            }

            if (!ReturnStatementHelper.IsReturnTypeIObservable(returnStatement))
            {
                return;
            }

            if (ReturnStatementHelper.IsReturnTypeOnlyIObservable(returnStatement))
            {
                return;
            }

            IExpression expression = returnStatement.Value as IReferenceExpression ?? returnStatement.Value;

            var range = expression.GetDocumentRange();
            var file = expression.GetContainingFile();

            var highlighting = new AsObservableHighlighting(expression);
            var info = new HighlightingInfo(range, highlighting, new Severity?());

            consumer.AddHighlighting(info.Highlighting, range, file);
        }
    }
}