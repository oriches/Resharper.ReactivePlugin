namespace Resharper.ReactivePlugin.ProblemAnalyzers
{
    using System;
    using System.Diagnostics;
    using Highlighters;
    using JetBrains.DocumentModel;
    using JetBrains.ReSharper.Daemon;
    using JetBrains.ReSharper.Daemon.Stages;
    using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.Tree;
    using JetBrains.Util;
    using Helpers;

    [ElementProblemAnalyzer(new[] { typeof(IObjectCreationExpression) }, HighlightingTypes = new[] { typeof(SchedulerHighlighting) })]
    public sealed class ConstructorWithSchedulerAnalyzer : ElementProblemAnalyzer<IObjectCreationExpression>
    {
        protected override void Run(IObjectCreationExpression expression, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            try
            {
                IConstructor constructor;
                if (!ConstructorHelper.IsContructor(expression, out constructor))
                {
                    // Only interested in constructors...
                    return;
                }

                if (SchedulerHelper.HasSchedulerParameter(constructor))
                {
                    // Scheduler has been defined nothing to check...
                    return;
                }

                if (!SchedulerHelper.HasOverloadWithSchedulerParameter(constructor))
                {
                    return;
                }

                // Overloaded version taking IScheduler as a parameter does exist
                // You should be using this overload so we create a highlight...

                var constructorName = expression.TypeName.QualifiedName;
                var text = expression.GetText();

                var range = expression.GetDocumentRange();
                var index = text.IndexOf(constructorName, StringComparison.Ordinal);
                if (index != -1)
                {
                    var startIndex = range.TextRange.StartOffset + index;
                    int endIndex;

                    if ((text.Length + Constants.NewOperator.Length) > Constants.HighlightLength)
                    {
                        startIndex = range.TextRange.StartOffset + Constants.NewOperator.Length;
                        endIndex = startIndex + Constants.HighlightLength;
                    }
                    else
                    {
                        endIndex = startIndex + range.TextRange.EndOffset;
                    }

                    var textRange = new TextRange(startIndex, endIndex);
                    range = new DocumentRange(expression.GetDocumentRange().Document, textRange);
                }

                var file = expression.GetContainingFile();
                var highlighting = new UndefinedSchedulerHighlighting(expression);
                var info = new HighlightingInfo(range, highlighting, new Severity?());

                consumer.AddHighlighting(info.Highlighting, range, file);
            }
            catch (Exception exn)
            {
                Debug.WriteLine("Failed ConstructorWithSchedulerAnalyzer, exception message - '{0}'", exn.Message);
            }
        }
    }
}