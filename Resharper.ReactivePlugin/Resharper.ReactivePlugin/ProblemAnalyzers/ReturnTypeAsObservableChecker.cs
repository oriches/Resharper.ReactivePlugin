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
                // It's not  return type IObservable<T>...
                return;
            }

            if (!ReturnStatementHelper.IsContainingDeclarationPublic(returnStatement))
            {
                // It's part of a public method or property...
                return;
            }

            if (ReturnStatementHelper.IsReturnTypeAsObservable(returnStatement))
            {
                // It's already calling AsObservable...
                return;
            }

            if (!ReturnStatementHelper.IsReturnTypeIObservable(returnStatement))
            {
                // The return type isn't IObservable<T>...
                return;
            }

            if (ReturnStatementHelper.IsReturnTypeOnlyIObservable(returnStatement))
            {
                // The return type can't be case to away from IObservable<T>...
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