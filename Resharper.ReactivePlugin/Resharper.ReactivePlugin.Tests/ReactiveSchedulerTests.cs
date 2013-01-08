namespace Resharper.ReactivePlugin.Tests
{
    using Highlighters;
    using JetBrains.Application.Settings;
    using JetBrains.ReSharper.Daemon;
    using JetBrains.ReSharper.TestFramework;
    using NUnit.Framework;

    [TestNetFramework45]
    [TestFixture]
    public class ReactiveSchedulerTests : ReactiveCSharpHighlightingTestBase
    {
        protected override bool HighlightingPredicate(IHighlighting highlighting, IContextBoundSettingsStore settingsstore)
        {
            return highlighting is SchedulerHighlighting;
        }

        protected override string RelativeTestDataPath
        {
            get { return @"scheduler"; }
        }

        [Test]
        [TestCase("file00.cs")]
        public void should_not_highlight_none_observable_methods(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file01.cs")]
        public void should_highlight_observable_method_which_can_take_scheduler(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file02.cs")]
        public void should_not_highlight_observable_method_which_has_scheduler_defined(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file03.cs")]
        public void should_not_highlight_observable_method_which_can_not_take_scheduler(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file04.cs")]
        public void should_highlight_multiple_observable_methods_which_can_take_schedulers(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file05.cs")]
        public void should_highlight_lambda_observable_method_which_can_take_scheduler(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file06.cs")]
        public void should_highlight_subject_constructor_which_can_take_scheduler(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file07.cs")]
        public void should_not_highlight_subject_constructor_which_has_scheduler(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file08.cs")]
        public void should_highlight_method_which_can_take_optional_scheduler(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file09.cs")]
        public void should_not_highlight_method_with_defined_optional_which_has_scheduler(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file10.cs")]
        public void should_highlight_constructor_which_can_take_scheduler(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file11.cs")]
        public void should_not_highlight_constructor_which_has_scheduler(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file12.cs")]
        public void should_not_highlight_method_which_can_not_take_scheduler(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file13.cs")]
        public void should_highlight_extension_method_which_can_take_scheduler(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file14.cs")]
        public void should_not_highlight_extension_method_which_has_scheduler(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file15.cs")]
        public void should_not_highlight_extension_method_which_can_not_take_scheduler(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file16.cs")]
        public void should_highlight_all_observable_method_name(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file17.cs")]
        public void should_highlight_all_observable_method_name_but_not_local_variable_name(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file18.cs")]
        public void should_not_highlight_observable_method_which_can_take_scheduler_for_parameterised_overload(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file19.cs")]
        public void should_highlight_observable_method_which_can_take_scheduler_for_parameterised_overload(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }

        [Test]
        [TestCase("file20.cs")]
        public void should_not_highlight_observable_method_which_can_not_take_scheduler_for_parameterised_overload(string testName)
        {
            using (ResolverReactiveAssemblies())
            {
                DoTestFiles(testName);
            }
        }
    }
}
