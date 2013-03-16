namespace Resharper.ReactivePlugin.ProblemAnalyzers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Helpers;
    using Highlighters;
    using JetBrains.DocumentModel;
    using JetBrains.ReSharper.Daemon;
    using JetBrains.ReSharper.Daemon.Stages;
    using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.Tree;
    using JetBrains.Util;

    [ElementProblemAnalyzer(new[] { typeof(IInvocationExpression) }, HighlightingTypes = new[] { typeof(SelectAndMergeHighlighting) })]
    public sealed class SelectAndMergeAnalyzer : ElementProblemAnalyzer<IInvocationExpression>
    {
        private const string MergeMethodName = "Merge";
        private const string SelectMethodName = "Select";

        protected override void Run(IInvocationExpression expression, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            try
            {
                var sourceFile = expression.GetSourceFile();
                if (sourceFile == null)
                {
                    // What not source file!
                    return;
                }

                var psiFile = sourceFile.GetNonInjectedPsiFile<CSharpLanguage>();
                if (psiFile == null)
                {
                    // It's not a CSharp file...
                    return;
                }

                var expressions = new List<IInvocationExpression>();
                psiFile.ProcessChildren<IInvocationExpression>(expressions.Add);

                var index = expressions.FindIndex(c => c == expression);
                var previousIndex = index + 1;

                if (previousIndex > (expressions.Count - 1))
                {
                    // There isn't a previous invocation expression...
                    return;
                }

                var previousExpression = expressions[previousIndex];

                IMethod currentMethod;
                if (!MethodHelper.IsMethod(expression, out currentMethod))
                {
                    // Isn't a method so continue...
                    return;
                }

                IMethod previousMethod;
                if (!MethodHelper.IsMethod(previousExpression, out previousMethod))
                {
                    // Isn't a method so continue...
                    return;
                }

                if (!MethodHelper.IsReturnTypeIObservable(currentMethod) ||
                    !MethodHelper.IsReturnTypeIObservable(previousMethod))
                {
                    // They both need to be reactive methods returning IObservable<T>...
                    return;
                }

                if (!MethodHelper.IsFromReactiveObservableClass(currentMethod) ||
                    !MethodHelper.IsFromReactiveObservableClass(previousMethod))
                {
                    // They aren't both method from the reactive assemblies...
                    return;
                }

                if (previousMethod.ShortName != SelectMethodName ||
                    currentMethod.ShortName != MergeMethodName)
                {
                    // The current method is not 'Select' or the next method is not 'Merge'...
                    return;
                }

                var firstChild = previousExpression.FirstChild;
                if (firstChild == null)
                {
                    return;
                }

                var previousRange = previousExpression.GetDocumentRange();
                var currentRange = expression.GetDocumentRange();

                var previousLastIndex = GetExpressionMethodIndex(previousExpression, previousMethod);

                var textRange = new TextRange(previousRange.TextRange.StartOffset + previousLastIndex,
                                              currentRange.TextRange.EndOffset);
                var range = new DocumentRange(expression.GetDocumentRange().Document, textRange);

                var file = expression.GetContainingFile();
                var highlighting = new SelectAndMergeHighlighting(expression);
                var info = new HighlightingInfo(range, highlighting, new Severity?());

                consumer.AddHighlighting(info.Highlighting, range, file);
            }
            catch (Exception exn)
            {
                Debug.WriteLine("Failed SelectAndMergeAnalyzer, exception message - '{0}'", exn.Message);
            }
        }

        private static int GetExpressionMethodIndex(IInvocationExpression expression, IMethod method)
        {
            var invokedText = expression.InvokedExpression.GetText();
            var lastIndex = invokedText.LastIndexOf(method.ShortName, StringComparison.Ordinal);

            return lastIndex;
        }
    }
}