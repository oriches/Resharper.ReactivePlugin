namespace Resharper.ReactivePlugin.QuickFixes
{
    using Highlighters;
    using JetBrains.ReSharper.Daemon;
    using JetBrains.ReSharper.Feature.Services.Bulbs;
    using JetBrains.ReSharper.Intentions.Extensibility;
    using JetBrains.ReSharper.Intentions.Extensibility.Menu;
    using JetBrains.Util;

    [QuickFix]
    public sealed class SelectAndMergeQuickFix : IQuickFix
    {
        private readonly SelectAndMergeHighlighting _highlighting;

        public SelectAndMergeQuickFix(SelectAndMergeHighlighting highlighting)
        {
            _highlighting = highlighting;
        }

        public bool IsAvailable(IUserDataHolder cache)
        {
            return _highlighting.IsValid();
        }

        public void CreateBulbItems(BulbMenu menu, Severity severity)
        {
            menu.ArrangeQuickFix(new SelectAndMergeBulbItem(_highlighting.Expression), severity);
        }
    }
}