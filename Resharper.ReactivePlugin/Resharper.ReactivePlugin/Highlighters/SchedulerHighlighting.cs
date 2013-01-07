using JetBrains.ReSharper.Daemon;
using Resharper.ReactivePlugin.Highlighters;

[assembly: RegisterConfigurableSeverity(SchedulerHighlighting.SeverityId,
  null,
  HighlightingGroupIds.CodeSmell,
  "Reactive extensions IScheduler instance is not defined.",
  "Not explicitly defining the IScheduler could introduce scheduling deadlocks, especially in UI based application where there is a single UI dispatcher thread.",
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
    public abstract class SchedulerHighlighting : IHighlightingWithRange
    {
        public const string SeverityId = "ReactiveSchedulerChecker";

        private readonly IExpression _expression;

        protected SchedulerHighlighting(IExpression expression)
        {
            _expression = expression;
        }

        public virtual string ToolTip
        {
            get { return string.Empty; }
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