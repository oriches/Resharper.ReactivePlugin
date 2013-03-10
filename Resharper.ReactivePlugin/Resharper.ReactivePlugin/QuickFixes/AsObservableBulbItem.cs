namespace Resharper.ReactivePlugin.QuickFixes
{
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
    using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
    using JetBrains.ReSharper.Psi.Tree;
    using JetBrains.TextControl;

    public sealed class AsObservableBulbItem : IBulbAction
    {
        private const string AsObservableNamespace = "System.Reactive.Linq";

        private readonly IExpression _literalExpression;

        public AsObservableBulbItem(IExpression literalExpression)
        {
            _literalExpression = literalExpression;
        }

        public string Text
        {
            get { return string.Format("Add 'AsObservable()' to return parameter"); }
        }

        public void Execute(ISolution solution, ITextControl textControl)
        {
            if (!_literalExpression.IsValid())
            {
                return;
            }

            var containingFile = _literalExpression.GetContainingFile();
            var elementFactory = CSharpElementFactory.GetInstance(_literalExpression.GetPsiModule());
            var psiManager = _literalExpression.GetPsiServices().PsiManager;
            var moduleManager = _literalExpression.GetPsiServices().ModuleManager;
            
           IExpression newExpression = null;
            psiManager.DoTransaction(
                () =>
                    {
                        using (solution.GetComponent<IShellLocks>().UsingWriteLock())
                        {
                            var replacementExpression = elementFactory.CreateExpression(_literalExpression.GetText() + ".AsObservable()");
                            newExpression = ModificationUtil.ReplaceChild(_literalExpression, replacementExpression);
                        }
                    }, GetType().Name);

            if (newExpression != null)
            {
                var marker = newExpression.GetDocumentRange().CreateRangeMarker(solution.GetComponent<DocumentManager>());
                containingFile.OptimizeImportsAndRefs(marker, false, true, NullProgressIndicator.Instance);
            }
        }
    }
}
