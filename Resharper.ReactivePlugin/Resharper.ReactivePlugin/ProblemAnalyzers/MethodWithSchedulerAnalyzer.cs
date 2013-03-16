namespace Resharper.ReactivePlugin.ProblemAnalyzers
{
    using System;
    using System.Diagnostics;
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
            try
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

                IParameter schedulerParameter;
                if (SchedulerHelper.HasSchedulerParameter(method, expression.ArgumentList, out schedulerParameter))
                {
                    // If it's optional is it not null...
                    if (schedulerParameter.IsOptional)
                    {
                        if (!System.Linq.Enumerable.Any(expression.ArgumentList.Arguments,
                                                        a => TypeHelper.HasISchedulerSuperType(a.Value.Type())))
                        {
                            // Scheduler parameter is not defined (null)...
                            CreateHighlightAndAddToConsumer(method, expression, consumer);
                        }
                    }
                }
                else
                {
                    if (!SchedulerHelper.HasOverloadWithSchedulerParameter(method, expression))
                    {
                        // Can't find an overload with the scheduler parameter...
                        return;
                    }

                    // Overloaded version taking IScheduler as a parameter does exist
                    // You could be using this overload so we create a highlight...
                    CreateHighlightAndAddToConsumer(method, expression, consumer);
                }
            }
            catch (Exception exn)
            {
                Debug.WriteLine("Failed MethodWithSchedulerAnalyzer, exception message - '{0}'", exn.Message);
            }
        }

        private static void CreateHighlightAndAddToConsumer(IMethod method, IInvocationExpression expression, IHighlightingConsumer consumer)
        {
            var methodName = method.ShortName;
            var invokedText = expression.InvokedExpression.GetText();

            var range = expression.GetDocumentRange();
            
            var lastIndex = invokedText.LastIndexOf(methodName, StringComparison.Ordinal);
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