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

    public sealed class SelectAndMergeBulbItem : IBulbAction
    {
        private readonly IExpression _literalExpression;

        public SelectAndMergeBulbItem(IExpression literalExpression)
        {
            _literalExpression = literalExpression;
        }

        public string Text
        {
            get { return string.Format("Replace methods 'Select' & 'Merge' with method 'SelectMany'"); }
        }

        public void Execute(ISolution solution, ITextControl textControl)
        {
            try
            {
                
            }
            catch (Exception exn)
            {
                Debug.WriteLine("Failed SelectAndMergeBulbItem, exception message - '{0}'", exn.Message);
            }
        }
    }
}
