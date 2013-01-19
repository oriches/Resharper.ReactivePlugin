namespace Resharper.ReactivePlugin.Tests
{
    using Helpers;
    using Highlighters;
    using JetBrains.Application.Settings;
    using JetBrains.ReSharper.Daemon;
    using JetBrains.ReSharper.TestFramework;
    using NUnit.Framework;

    [TestNetFramework45]
    [TestFixture]
    public class AsObservableTests : ReactiveHighlightingTestBase
    {
        protected override bool HighlightingPredicate(IHighlighting highlighting, IContextBoundSettingsStore settingsstore)
        {
            return highlighting is AsObservableHighlighting;
        }

        protected override string RelativeTestDataPath
        {
            get { return @"as_observable"; }
        }

        [Test]
        [TestCase("file00.cs")]
        public void should_not_highlight_none_observable_methods_and_properties(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file01.cs")]
        public void should_highlight_missing_as_observable_for_subject_type_returned_as_iobservable_in_public_method(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file02.cs")]
        public void should_not_highlight_as_observable_for_subject_type_returned_as_iobservable_in_public_method(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file03.cs")]
        public void should_not_highlight_as_observable_for_subject_type_returned_as_iobservable_in_private_method(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file04.cs")]
        public void should_highlight_missing_as_observable_for_custom_type_returned_as_iobservable_in_public_method(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file05.cs")]
        public void should_not_highlight_as_observable_for_custom_type_returned_as_iobservable_in_public_method(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file06.cs")]
        public void should_not_highlight_as_observable_for_custom_type_returned_as_iobservable_in_private_method(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file10.cs")]
        public void should_not_highlight_as_observable_for_subject_type_returned_as_iobservable_in_public_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file09.cs")]
        public void should_highlight_missing_as_observable_for_custom_type_returned_as_iobservable_in_public_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file11.cs")]
        public void should_not_highlight_as_observable_for_custom_type_returned_as_iobservable_in_private_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file12.cs")]
        public void should_not_highlight_as_observable_for_subject_type_returned_as_iobservable_in_private_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file13.cs")]
        public void should_highlight_missing_as_observable_for_subject_type_returned_as_iobservable_in_public_auto_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file14.cs")]
        public void should_highlight_missing_as_observable_for_custom_type_returned_as_iobservable_in_public_auto_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file15.cs")]
        public void should_not_highlight_as_observable_for_subject_type_returned_as_iobservable_in_private_auto_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file16.cs")]
        public void should_not_highlight_as_observable_for_custom_type_returned_as_iobservable_in_private_auto_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file17.cs")]
        public void should_not_highlight_as_observable_for_custom_type_returned_as_iobservable_in_public_auto_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file18.cs")]
        public void should_not_highlight_as_observable_for_custom_type_returned_as_iobservable_in_public_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file19.cs")]
        public void should_not_highlight_as_observable_for_subject_type_returned_as_iobservable_in_public_auto_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file20.cs")]
        public void should_not_highlight_as_observable_for_return_type_in_public_method_which_only_exposes_iobservable(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file21.cs")]
        public void should_not_highlight_as_observable_for_custom_type_returned_as_custom_type_in_public_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file22.cs")]
        public void should_highlight_missing_as_observable_for_subject_type_returned_as_iobservable_in_public_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file23.cs")]
        public void should_not_highlight_as_observable_for_subject_type_returned_as_subject_type_in_public_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file24.cs")]
        public void should_not_highlight_as_observable_for_subject_type_returned_as_subject_type_in_public_method(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file25.cs")]
        public void should_not_highlight_as_observable_for_custom_type_returned_as_custom_type_in_public_method(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file26.cs")]
        public void should_not_highlight_as_observable_for_custom_type_returned_as_custom_type_in_public_auto_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file27.cs")]
        public void should_not_highlight_as_observable_for_subject_type_returned_as_subject_type_in_public_auto_property(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }
    }
}