namespace Resharper.ReactivePlugin.ContextActions
{
    using System;
    using JetBrains.Application.Progress;
    using JetBrains.ProjectModel;
    using JetBrains.ReSharper.Feature.Services.Bulbs;
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
    using JetBrains.ReSharper.Feature.Services.LinqTools;
    using JetBrains.ReSharper.Intentions.Extensibility;
    using JetBrains.ReSharper.Intentions.Extensibility.Menu;
    using JetBrains.ReSharper.Psi.CSharp;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.Tree;
    using JetBrains.TextControl;
    using JetBrains.Util;

    [ContextAction(Name = "UseSelectMany", Description = "Use SelectMany instead of Select & Merge", Group = "C#")]
    public class UseSelectManyInsteadOfMerge : ContextActionBase, IContextAction
    {
        private readonly ICSharpContextActionDataProvider _provider;
        private ILiteralExpression _stringLiteral;

        public UseSelectManyInsteadOfMerge(ICSharpContextActionDataProvider provider)
        {
            _provider = provider;
        }

        public override bool IsAvailable(IUserDataHolder cache)
        {
            var literal = _provider.GetSelectedElement<ILiteralExpression>(true, true);
            if (literal != null && literal.IsConstantValue() && literal.ConstantValue.IsString())
            {
                var s = literal.ConstantValue.Value as string;
                if (!string.IsNullOrEmpty(s))
                {
                    _stringLiteral = literal;
                    return true;
                }
            }
            return false;
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            CSharpElementFactory factory = CSharpElementFactory.GetInstance(_provider.PsiModule);

            var stringValue = _stringLiteral.ConstantValue.Value as string;
            if (stringValue == null)
                return null;

            var chars = stringValue.ToCharArray();
            Array.Reverse(chars);
            ICSharpExpression newExpr = factory.CreateExpressionAsIs("\"" + new string(chars) + "\"");
            _stringLiteral.ReplaceBy(newExpr);
            return null;
        }

        public override string Text
        {
            get { return "UseSelectMany"; }
        }
    }



    [ContextAction(Description = "Use SelectMany instead of Select & Merge",
      Group = "C#",
      Name = "UseSelectMany")]
    public sealed class UseSelectManyInsteadOfMergeAction : IContextAction
    {
        private readonly ICSharpContextActionDataProvider _provider;
        private IBulbAction[] _items;

        /// <summary>
        /// For languages other than C# any inheritor of <see cref="IContextActionDataProvider"/> can 
        /// be injected in this constructor.
        /// </summary>
        public UseSelectManyInsteadOfMergeAction(ICSharpContextActionDataProvider provider)
        {
            _provider = provider;
        }

        public void CreateBulbItems(BulbMenu menu)
        {
            // Basically 
            foreach (var item in Items)
            {
                // use other extension methods on BulbMenu to arrange menu items.
                menu.ArrangeContextAction(item);
            }
        }

        public bool IsAvailable(IUserDataHolder cache)
        {
            // Availability code may be optimized but for most cases can be as simple as follow:
            return Items.Length > 0;
        }

        private IBulbAction[] Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new IBulbAction[]
                   {
                     new MyBulbItem(_provider)
                   };
                }
                return _items;
            }
        }
    }

    public class MyBulbItem : BulbActionBase
    {
        private readonly ICSharpContextActionDataProvider _provider;

        public MyBulbItem(ICSharpContextActionDataProvider provider)
        {
            _provider = provider;
        }

        public override string Text
        {
            get
            {
                // text returned here will be displayed on the context action
                return "Use SelectMany instead of Select & Merge";
            }
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            // put transacted steps here
            // use 'provider' field to get currently selected element

            return textControl =>
            {
                // put post-transaction steps here
                // if there are none, you can replace this with 'return null'
            };
        }

    }
}
