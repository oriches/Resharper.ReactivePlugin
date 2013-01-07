namespace Resharper.ReactivePlugin.ProblemAnalyzers
{
    using System;
    using Helpers;
    using Highlighters;
    using JetBrains.DocumentModel;
    using JetBrains.ReSharper.Daemon;
    using JetBrains.ReSharper.Daemon.Stages;
    using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.Tree;
    using JetBrains.Util;

    [ElementProblemAnalyzer(new[] { typeof(IAssignmentExpression) }, HighlightingTypes = new[] { typeof(AsObservableHighlighting) })]
    public sealed class AutoPropertyAsObservableChecker : ElementProblemAnalyzer<IAssignmentExpression>
    {
        protected override void Run(IAssignmentExpression expression, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            IProperty property;
            if (!PropertyHelper.IsPropertyAssignment(expression, out property))
            {
                return;
            }

            if (!PropertyHelper.HasPublicModifier(property))
            {
                return;
            }

            if (!PropertyHelper.IsAutoProperty(property))
            {
                return;
            }

            if (PropertyHelper.IsSourceAssignmentAsObservable(expression))
            {
                return;
            }

            if (!TypeHelper.HasIObservableSuperType(property.ReturnType))
            {
                return;
            }

            var assignmentText = expression.GetText();
            var sourceText = expression.Source.GetText();
            
            var range = expression.GetDocumentRange();
            var index = assignmentText.LastIndexOf(sourceText, StringComparison.Ordinal);
            if (index != 0)
            {
                var textRange = new TextRange(range.TextRange.StartOffset + index, range.TextRange.EndOffset);
                range = new DocumentRange(expression.GetDocumentRange().Document, textRange);
            }

            var file = expression.GetContainingFile();

            var highlighting = new AsObservableHighlighting(expression);
            var info = new HighlightingInfo(range, highlighting, new Severity?());

            consumer.AddHighlighting(info.Highlighting, range, file);
        }
    }
}