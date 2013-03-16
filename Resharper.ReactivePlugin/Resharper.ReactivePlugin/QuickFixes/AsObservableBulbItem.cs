namespace Resharper.ReactivePlugin.QuickFixes
{
    using System;
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
            try
            {
                if (!_literalExpression.IsValid())
                {
                    return;
                }

                var psiModule = _literalExpression.GetPsiModule();
                var psiManager = _literalExpression.GetPsiServices().PsiManager;

                var containingFile = _literalExpression.GetContainingFile();
                var csharpFile = containingFile as ICSharpFile;

                var elementFactory = CSharpElementFactory.GetInstance(psiModule);

                IExpression newExpression = null;
                psiManager.DoTransaction(
                    () =>
                        {
                            using (solution.GetComponent<IShellLocks>().UsingWriteLock())
                            {
                                var replacementExpression =
                                    elementFactory.CreateExpression(_literalExpression.GetText() + ".AsObservable()");
                                newExpression = ModificationUtil.ReplaceChild(_literalExpression, replacementExpression);

                                EnsureNamespaceExists(csharpFile, elementFactory);
                            }
                        }, GetType().Name);

                if (newExpression != null)
                {
                    var marker = newExpression.GetDocumentRange().CreateRangeMarker(solution.GetComponent<DocumentManager>());
                    containingFile.OptimizeImportsAndRefs(marker, false, true, NullProgressIndicator.Instance);
                }
            }
            catch (Exception exn)
            {
                Debug.WriteLine("Failed AsObservableBulbItem, exception message - '{0}'", exn.Message);
            }
        }

        private static void EnsureNamespaceExists(ICSharpFile file, CSharpElementFactory factory)
        {
            var namespaceExists =
                file.NamespaceDeclarationNodes.Any(
                    n => n.Imports.Any(d => d.ImportedSymbolName.QualifiedName == AsObservableNamespace));

            if (!namespaceExists)
            {
                var directive = factory.CreateUsingDirective(AsObservableNamespace);

                var namespaceNode = file.NamespaceDeclarationNodes.FirstOrDefault();
                if (namespaceNode != null)
                {
                    UsingUtil.AddImportTo(namespaceNode, directive);
                }
                else
                {
                    UsingUtil.AddImportTo(file, directive);
                }
            }
        }
    }
}
