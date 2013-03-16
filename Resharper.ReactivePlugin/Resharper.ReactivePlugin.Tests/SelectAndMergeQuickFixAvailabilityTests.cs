namespace Resharper.ReactivePlugin.Tests
{
    using Helpers;
    using Highlighters;
    using JetBrains.ReSharper.Daemon;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.TestFramework;
    using NUnit.Framework;

    [TestNetFramework4]
    [TestFixture]
    public class SelectAndMergeQuickFixAvailabilityTests : ReactiveQuickFixAvailabilityTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"select_and_merge_quick_fix"; }
        }

        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile psiSourceFile)
        {
            return highlighting is SelectAndMergeHighlighting;
        }

        [Test]
        [TestCase("availability01.cs")]
        public void will_highlight_quick_fix_select_and_merge_availability(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }
    }
}