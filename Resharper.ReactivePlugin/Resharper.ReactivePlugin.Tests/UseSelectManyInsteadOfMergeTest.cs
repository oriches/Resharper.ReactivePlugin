namespace Resharper.ReactivePlugin.Tests
{
    using Helpers;
    using NUnit.Framework;

    [TestFixture]
    public class UseSelectManyInsteadOfMergeTest : ReactiveCodeCleanupTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"select_many"; }
        }

        [Test]
        public void should_cleanup_select_merge_into_selectmany()
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles("file01.cs");
            }
        }

        [Test]
        public void should_cleanup_select_merge_into_selectmany_without_cleanup()
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFilesWithProfile("disableCleanup.profile", "file01_disable.cs");
            }
        }
    }
}
