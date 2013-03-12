namespace Resharper.ReactivePlugin.Highlighters
{
    using JetBrains.ReSharper.Daemon;
    using JetBrains.ReSharper.Psi.CSharp;
    using JetBrains.ReSharper.Psi.Tree;

    [ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name, OverlapResolve = OverlapResolveKind.WARNING)]
    public sealed class UndefinedSchedulerHighlighting : SchedulerHighlighting
    {
        private const string ToolTipInfo = "Consider explicitly defining the Scheduler, this can help reduce threading issues and improve testing.";

        public UndefinedSchedulerHighlighting(IExpression expression) : base(expression)
        {
        }

        public override string ToolTip
        {
            get { return ToolTipInfo; }
        }
    }
}