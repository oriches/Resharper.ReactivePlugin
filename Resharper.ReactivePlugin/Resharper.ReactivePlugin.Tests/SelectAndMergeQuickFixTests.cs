namespace Resharper.ReactivePlugin.Tests
{
    using Helpers;
    using JetBrains.ReSharper.TestFramework;
    using NUnit.Framework;
    using QuickFixes;

    [TestNetFramework4]
    [TestFixture]
    public class SelectAndMergeQuickFixTests : ReactiveCSharpQuickFixTestBase<SelectAndMergeQuickFix>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"select_and_merge_quick_fix"; }
        }

        [Test]
        [TestCase("file01.cs")]
        public void will_quick_fix_observable_methods_select_and_empty_merge_which_can_be_replaced_with_selectmany(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file02.cs")]
        public void will_quick_fix_observable_methods_select_and_simple_merge_which_can_be_replaced_with_selectmany(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file03.cs")]
        public void will_quick_fix_observable_methods_select_and_merge_when_there_are_multiple_merges_which_can_be_replaced_with_selectmany(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }
    }
}