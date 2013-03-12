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

    [ElementProblemAnalyzer(new[] { typeof(IInvocationExpression) }, HighlightingTypes = new[] { typeof(SelectAndMergeHighlighting) })]
    public sealed class SelectAndMergeAnalyzer : ElementProblemAnalyzer<IInvocationExpression>
    {
        private const string MergeMethodName = "Merge";
        private const string SelectMethodName = "Select";

        private static readonly CurrentAndPreviousState<Tuple<IInvocationExpression, IMethod>> _state = new CurrentAndPreviousState<Tuple<IInvocationExpression, IMethod>>();

        protected override void Run(IInvocationExpression expression, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            _state.InitialiseForFile(expression.GetSourceFile());

            IMethod newMethod;
            if (!MethodHelper.IsMethod(expression, out newMethod))
            {
                // Isn't a method so continue...
                _state.SetCurrent(null);

                return;
            }

            _state.SetCurrent(new Tuple<IInvocationExpression, IMethod>(expression, newMethod));

            // Is the current method and next method in the expression
            // suitale to cleaned up into a SelectMany...
            if (_state.Current == null || _state.Previous == null)
            {
                // Neither the current or next expression are methods...
                return;
            }

            if (!MethodHelper.IsReturnTypeIObservable(_state.Current.Item2) ||
                !MethodHelper.IsReturnTypeIObservable(_state.Previous.Item2))
            {
                // They both need to be reactive methods returning IObservable<T>...
                _state.SetCurrent(null);

                return;
            }

            if (!MethodHelper.IsFromReactiveObservableClass(_state.Current.Item2) ||
                !MethodHelper.IsFromReactiveObservableClass(_state.Previous.Item2))
            {
                // They aren't both method from the reactive assemblies...
                return;
            }

            if (_state.Previous.Item2.ShortName != SelectMethodName ||
                _state.Current.Item2.ShortName != MergeMethodName)
            {
                // The current method is not 'Select' or the next method is not 'Merge'...
                return;
            }

            var firstChild = _state.Previous.Item1.FirstChild;
            if (firstChild == null)
            {
                return;
            }

            var previousRange = _state.Previous.Item1.GetDocumentRange();
            var currentRange = _state.Current.Item1.GetDocumentRange();

            var previousLastIndex = GetExpressionMethodIndex(_state.Previous.Item1, _state.Previous.Item2);

            var textRange = new TextRange(previousRange.TextRange.StartOffset + previousLastIndex, currentRange.TextRange.EndOffset);
            var range = new DocumentRange(expression.GetDocumentRange().Document, textRange);

            var file = expression.GetContainingFile();
            var highlighting = new SelectAndMergeHighlighting(expression);
            var info = new HighlightingInfo(range, highlighting, new Severity?());

            consumer.AddHighlighting(info.Highlighting, range, file);

            _state.Reset();
        }

        private static int GetExpressionMethodIndex(IInvocationExpression expression, IMethod method)
        {
            var invokedText = expression.InvokedExpression.GetText();
            var lastIndex = invokedText.LastIndexOf(method.ShortName, StringComparison.Ordinal);

            return lastIndex;
        }
    }
}