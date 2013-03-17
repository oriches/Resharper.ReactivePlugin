namespace Resharper.ReactivePlugin.QuickFixes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using JetBrains.Application;
    using JetBrains.Application.Progress;
    using JetBrains.DocumentManagers;
    using JetBrains.ProjectModel;
    using JetBrains.ReSharper.Intentions.Extensibility;
    using JetBrains.ReSharper.Psi.CSharp;
    using JetBrains.ReSharper.Psi.CSharp.CodeStyle;
    using JetBrains.ReSharper.Psi.CSharp.Impl;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
    using JetBrains.ReSharper.Psi.Tree;
    using JetBrains.TextControl;

    public sealed class SelectAndMergeBulbItem : IBulbAction
    {
        private readonly IExpression _expression;

        public SelectAndMergeBulbItem(IExpression literalExpression)
        {
            _expression = literalExpression;
        }

        public string Text
        {
            get { return string.Format("Replace methods 'Select' & 'Merge' with method 'SelectMany'"); }
        }

        public void Execute(ISolution solution, ITextControl textControl)
        {
            try
            {
//                var expressions = new List<IInvocationExpression>();
//                psiFile.ProcessChildren<IInvocationExpression>(expressions.Add);
//
//                var index = expressions.FindIndex(c => c == _expression);
//                var previousIndex = index + 1;
//
//                if (previousIndex > (expressions.Count - 1))
//                {
//                    // There isn't a previous invocation expression...
//                    return;
//                }
//
//                var previousExpression = expressions[previousIndex];
//
//                var firstChild = _previousExpression.FirstChild;
//                if (firstChild == null)
//                {
//                    return;
//                }
//
//                var arugmentList = _previousExpression.Children<IArgumentList>().SingleOrDefault();
//                if (arugmentList == null)
//                {
//                    return;
//                }
//
//                var preText = firstChild.GetText();
//                var arugmentListText = arugmentList.GetText();
//
//                var lastSelectIndex = preText.LastIndexOf(SelectMethodName, StringComparison.Ordinal);
//                var formattedPreText = preText.Remove(lastSelectIndex, SelectMethodName.Length)
//                                              .Insert(lastSelectIndex, SelectManyMethodName);
//
//                var replacementExpressionText = string.Format(ReplacementTextFormat, formattedPreText, arugmentListText);
//                var replacementExpression = elementFactory.CreateExpression(replacementExpressionText);
//                ModificationUtil.ReplaceChild(_nextExpression, replacementExpression);
            }
            catch (Exception exn)
            {
                Debug.WriteLine("Failed SelectAndMergeBulbItem, exception message - '{0}'", exn.Message);
            }
        }
    }
}
