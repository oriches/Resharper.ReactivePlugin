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
    public class AsObservableQuickFixAvailabilityTests : ReactiveQuickFixAvailabilityTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"as_observable_quick_fix"; }
        }

        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile psiSourceFile)
        {
            return highlighting is AsObservableHighlighting;
        }

        [Test]
        [TestCase("availability01.cs")]
        public void will_highlight_quick_fix_as_observable_availability(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }
    }
}