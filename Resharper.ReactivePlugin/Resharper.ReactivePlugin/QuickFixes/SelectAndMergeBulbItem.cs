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
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp;
    using JetBrains.ReSharper.Psi.CSharp.CodeStyle;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
    using JetBrains.ReSharper.Psi.Tree;
    using JetBrains.TextControl;

    public sealed class SelectAndMergeBulbItem : IBulbAction
    {
        private const string ReplacementTextFormat = "{0}({1})";

        private readonly IExpression _expression;

        public SelectAndMergeBulbItem(IExpression expression)
        {
            _expression = expression;
        }

        public string Text
        {
            get { return "Replace methods 'Select' & 'Merge' with method 'SelectMany'"; }
        }

        public void Execute(ISolution solution, ITextControl textControl)
        {
            try
            {
                var psiManager = _expression.GetPsiServices().PsiManager;
                var sourceFile = _expression.GetSourceFile();
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

                var index = expressions.FindIndex(c => c == _expression);
                var previousIndex = index + 1;

                if (previousIndex > (expressions.Count - 1))
                {
                    // There isn't a previous invocation expression...
                    return;
                }

                var previousExpression = expressions[previousIndex];

                var firstChild = previousExpression.FirstChild;
                if (firstChild == null)
                {
                    return;
                }

                var arugmentList = previousExpression.Children<IArgumentList>().SingleOrDefault();
                if (arugmentList == null)
                {
                    return;
                }

                var containingFile = _expression.GetContainingFile();

                var elementFactory = CSharpElementFactory.GetInstance(sourceFile.PsiModule);

                IExpression newExpression = null;
                psiManager.DoTransaction(
                    () =>
                        {
                            using (solution.GetComponent<IShellLocks>().UsingWriteLock())
                            {
                                var preText = firstChild.GetText();
                                var arugmentListText = arugmentList.GetText();

                                var lastSelectIndex = preText.LastIndexOf(Constants.SelectMethodName,
                                                                          StringComparison.Ordinal);
                                var formattedPreText = preText.Remove(lastSelectIndex, Constants.SelectMethodName.Length)
                                                              .Insert(lastSelectIndex, Constants.SelectManyMethodName);

                                var replacementExpressionText = string.Format(ReplacementTextFormat, formattedPreText,
                                                                              arugmentListText);
                                var replacementExpression = elementFactory.CreateExpression(replacementExpressionText);

                                newExpression = ModificationUtil.ReplaceChild(_expression, replacementExpression);
                            }
                        }, GetType().Name);

                if (newExpression != null)
                {
                    var marker =
                        newExpression.GetDocumentRange().CreateRangeMarker(solution.GetComponent<DocumentManager>());
                    containingFile.OptimizeImportsAndRefs(marker, false, true, NullProgressIndicator.Instance);
                }
            }
            catch (Exception exn)
            {
                Debug.WriteLine("Failed SelectAndMergeBulbItem, exception message - '{0}'", exn.Message);
            }
        }
    }
}
