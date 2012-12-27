using JetBrains.ReSharper.Daemon;
using Resharper.ReactivePlugin.Highlights;

[assembly: RegisterConfigurableSeverity(AsObservableHighlighting.SeverityId,
  null,
  HighlightingGroupIds.CodeSmell,
  "Reactive extensions AsObservable is not called.",
  "AsObservable hides the identity of an observable sequence",
  Severity.SUGGESTION,
  false)]

namespace Resharper.ReactivePlugin.Highlights
{
    using JetBrains.ReSharper.Daemon;
    using JetBrains.DocumentModel;
    using JetBrains.ReSharper.Daemon.Impl;
    using JetBrains.ReSharper.Psi.CSharp;
    using JetBrains.ReSharper.Psi.Tree;

    [ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name, OverlapResolve = OverlapResolveKind.WARNING)]
    public sealed class AsObservableHighlighting : IHighlightingWithRange
    {
        public const string SeverityId = "AsObservableChecker";

        private const string ToolTipInfo = "Consider calling method AsObservable to prevent casting return type back to the declaring type, AsObservable hides the identity of an observable sequence.";

        private readonly IExpression _expression;

        public AsObservableHighlighting(IExpression expression)
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

        public bool IsValid()
        {
            return _expression == null || _expression.IsValid();
        }
    }
}