namespace Resharper.ReactivePlugin.Tests
{
    using Helpers;
    using JetBrains.ReSharper.TestFramework;
    using NUnit.Framework;
    using QuickFixes;

    [TestNetFramework4]
    [TestFixture]
    public class AsObservableQuickFixTests : ReactiveCSharpQuickFixTestBase<AsObservableQuickFix>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"as_observable_quick_fix"; }
        }

        [Test]
        [TestCase("file01.cs")]
        public void will_quick_fix_public_method_with_as_observable_on_iobservable_return_type(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file02.cs")]
        public void will_quick_fix_public_property_with_as_observable_on_iobservable_return_type(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file03.cs")]
        public void will_add_namespace_reference_when_as_observable_quick_fix_applied(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }
    }
}
