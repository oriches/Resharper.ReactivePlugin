namespace Resharper.ReactivePlugin.Tests
{
    using Helpers;
    using Highlighters;
    using JetBrains.Application.Settings;
    using JetBrains.ReSharper.Daemon;
    using JetBrains.ReSharper.TestFramework;
    using NUnit.Framework;

    [TestNetFramework4]
    [TestFixture]
    public class SelectAndMergeTests : ReactiveHighlightingTestBase
    {
        protected override bool HighlightingPredicate(IHighlighting highlighting, IContextBoundSettingsStore settingsstore)
        {
            return highlighting is SelectAndMergeHighlighting;
        }

        protected override string RelativeTestDataPath
        {
            get { return @"select_and_merge"; }
        }

        [Test]
        [TestCase("file00.cs")]
        public void will_not_highlight_none_observable_methods(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file01.cs")]
        public void will_highlight_observable_methods_select_and_empty_merge_which_can_be_replaced_with_selectmany(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file02.cs")]
        public void will_highlight_observable_methods_select_and_simple_merge_which_can_be_replaced_with_selectmany(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file03.cs")]
        public void will_highlight_observable_methods_select_and_merge_when_there_are_multiple_merges_which_can_be_replaced_with_selectmany(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }
    }
}