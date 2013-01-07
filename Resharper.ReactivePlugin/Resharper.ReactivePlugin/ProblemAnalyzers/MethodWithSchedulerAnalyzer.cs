﻿namespace Resharper.ReactivePlugin.ProblemAnalyzers
{
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

    [ElementProblemAnalyzer(new[] { typeof(IInvocationExpression) }, HighlightingTypes = new[] { typeof(SchedulerHighlighting) })]
    public sealed class MethodWithSchedulerAnalyzer : ElementProblemAnalyzer<IInvocationExpression>
    {
        protected override void Run(IInvocationExpression expression, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            IMethod method;
            if (!MethodHelper.IsMethod(expression, out method))
            { 
                // Only interested in methods...
                return;
            }

            if (!MethodHelper.IsReturnTypeIObservable(method))
            {
                // Only interested in methods returning IObservable<T>...
                return;
            }

            if (SchedulerHelper.HasPopulatedSchedulerParameter(method, expression.ArgumentList))
            {
                // Scheduler has been defined nothing to check...
                return;
            }

            if (!SchedulerHelper.HasOverloadWithSchedulerParameter(method))
            {
                return;
            }

            // Overloaded version taking IScheduler as a parameter does exist
            // You could be using this overload so we create a highlight...

            var methodName = method.ShortName;
            var invokedText = expression.InvokedExpression.GetText();
            var lastIndex = invokedText.LastIndexOf(methodName, System.StringComparison.Ordinal);

            var range = expression.GetDocumentRange();
            if (lastIndex != 0)
            {
                var textRange = new TextRange(range.TextRange.StartOffset + lastIndex, range.TextRange.EndOffset);
                range = new DocumentRange(expression.GetDocumentRange().Document, textRange);
            }
            
            var file = expression.GetContainingFile();
            var highlighting = new UndefinedSchedulerHighlighting(expression);
            var info = new HighlightingInfo(range, highlighting, new Severity?());

            consumer.AddHighlighting(info.Highlighting, range, file);
        }
    }
}