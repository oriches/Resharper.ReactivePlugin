namespace Resharper.ReactivePlugin.ProblemAnalyzers
{
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
            var index = text.IndexOf(constructorName, System.StringComparison.Ordinal);
            if (index != -1)
            {
                var textRange = new TextRange(range.TextRange.StartOffset + index, range.TextRange.EndOffset);
                range = new DocumentRange(expression.GetDocumentRange().Document, textRange);
            }

            var file = expression.GetContainingFile();
            var highlighting = new UndefinedSchedulerHighlighting(expression);
            var info = new HighlightingInfo(range, highlighting, new Severity?());

            consumer.AddHighlighting(info.Highlighting, range, file);
        }
    }
}