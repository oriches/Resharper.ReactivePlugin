namespace Resharper.ReactivePlugin.ProblemAnalyzers
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using Highlighters;
    using JetBrains.DocumentModel;
    using JetBrains.ReSharper.Daemon;
    using JetBrains.ReSharper.Daemon.Stages;
    using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.Tree;
    using Helpers;
    using JetBrains.Util;

    [ElementProblemAnalyzer(new[] { typeof(IReturnStatement) }, HighlightingTypes = new[] { typeof(AsObservableHighlighting) })]
    public sealed class ReturnTypeAsObservableAnalyzer : ElementProblemAnalyzer<IReturnStatement>
    {
        protected override void Run(IReturnStatement returnStatement, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            try
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

                var length = range.TextRange.EndOffset - range.TextRange.StartOffset;
                if (length > Constants.HighlightLength)
                {
                    var startIndex = range.TextRange.StartOffset;
                    if (range.GetText().StartsWith(Constants.NewOperator, true, CultureInfo.InvariantCulture))
                    {
                        startIndex = range.TextRange.StartOffset + Constants.NewOperator.Length;
                    }
                    
                    var endIndex = startIndex + Constants.HighlightLength;

                    var textRange = new TextRange(startIndex, endIndex);
                    range = new DocumentRange(expression.GetDocumentRange().Document, textRange);
                }

                var highlighting = new AsObservableHighlighting(expression);
                var info = new HighlightingInfo(range, highlighting, new Severity?());

                consumer.AddHighlighting(info.Highlighting, range, file);
            }
            catch (Exception exn)
            {
                Debug.WriteLine("Failed ReturnTypeAsObservableAnalyzer, exception message - '{0}'", exn.Message);
            }
        }
    }
}