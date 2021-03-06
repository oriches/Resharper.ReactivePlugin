﻿namespace Resharper.ReactivePlugin.ProblemAnalyzers
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
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
    public sealed class AutoPropertyAsObservableAnalyzer : ElementProblemAnalyzer<IAssignmentExpression>
    {
        protected override void Run(IAssignmentExpression expression, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            try
            {
                IProperty property;
                if (!PropertyHelper.IsPropertyAssignment(expression, out property))
                {
                    // It's not a proerties...
                    return;
                }

                if (!PropertyHelper.HasPublicModifier(property))
                {
                    // It's not a public properties...
                    return;
                }

                if (!PropertyHelper.IsAutoProperty(property))
                {
                    // It's not a public auto properties...
                    return;
                }

                if (!TypeHelper.IsIObservableType(property.ReturnType))
                {
                    // The return isn't IObservable<T>...
                    return;
                }

                if (PropertyHelper.IsSourceAssignmentAsObservable(expression))
                {
                    // It's already calling AsObservable()...
                    return;
                }

                var assignmentText = expression.GetText();
                var sourceText = expression.Source.GetText();
            
                var range = expression.GetDocumentRange();
                var index = assignmentText.LastIndexOf(sourceText, StringComparison.Ordinal);
                if (index != 0)
                {
                    int startIndex;
                    int endIndex;

                    if (range.GetText().IndexOf(Constants.NewOperator, StringComparison.InvariantCultureIgnoreCase) == index)
                    {
                        startIndex = range.TextRange.StartOffset + Constants.NewOperator.Length + index;
                    }
                    else
                    {
                        startIndex = range.TextRange.StartOffset + index;
                    }
                    
                    var length = range.TextRange.EndOffset - range.TextRange.StartOffset;
                    if (length > Constants.HighlightLength)
                    {
                        endIndex = startIndex + Constants.HighlightLength;
                    }
                    else
                    {
                        endIndex = range.TextRange.EndOffset;
                    }

                    var textRange = new TextRange(startIndex, endIndex);
                    range = new DocumentRange(expression.GetDocumentRange().Document, textRange);
                }

                var file = expression.GetContainingFile();

                var highlighting = new AsObservableHighlighting(expression);
                var info = new HighlightingInfo(range, highlighting, new Severity?());

                consumer.AddHighlighting(info.Highlighting, range, file);
            }
            catch (Exception exn)
            {
                Debug.WriteLine("Failed AutoPropertyAsObservableAnalyzer, exception message - '{0}'", exn.Message);
            }
        }
    }
}