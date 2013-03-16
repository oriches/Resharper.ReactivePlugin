using JetBrains.ReSharper.Daemon;
using Resharper.ReactivePlugin.Highlighters;

[assembly: RegisterConfigurableSeverity(SelectAndMergeHighlighting.SeverityId,
  null,
  HighlightingGroupIds.CodeSmell,
  "Reactive extensions 'Select' & 'Merge' can be replaced by a single call to 'SelectMany'.",
  "'Select' & 'Merge can be replaced by 'SelectMany'",
  Severity.SUGGESTION,
  false)]

namespace Resharper.ReactivePlugin.Highlighters
{
    using JetBrains.ReSharper.Daemon;
    using JetBrains.DocumentModel;
    using JetBrains.ReSharper.Daemon.Impl;
    using JetBrains.ReSharper.Psi.CSharp;
    using JetBrains.ReSharper.Psi.Tree;

    [ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name, OverlapResolve = OverlapResolveKind.WARNING)]
    public sealed class SelectAndMergeHighlighting : IHighlightingWithRange
    {
        public const string SeverityId = "ReactiveSelectAndMergeChecker";

        private const string ToolTipInfo = "Consider combining the methods 'Select' & 'Merge' into to the single method 'SelectMany'.";

        private readonly IExpression _expression;

        public SelectAndMergeHighlighting(IExpression expression)
        {
            _expression = expression;
        }

        public string ToolTip
        {
            get { return ToolTipInfo; }
        }

        public string ErrorStripeToolTip
        {
            get { return ToolTip; }
        }

        public int NavigationOffsetPatch
        {
            get { return 0; }
        }

        public DocumentRange CalculateRange()
        {
            return _expression.GetDocumentRange();
        }

        public IExpression Expression
        {
            get { return _expression; }
        }

        public bool IsValid()
        {
            return _expression == null || _expression.IsValid();
        }
    }
}