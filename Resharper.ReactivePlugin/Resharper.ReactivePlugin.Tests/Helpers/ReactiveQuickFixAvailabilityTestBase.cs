namespace Resharper.ReactivePlugin.Tests.Helpers
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using JetBrains.ProjectModel.Test.Components;
    using JetBrains.ReSharper.Intentions.Test;
    using JetBrains.ReSharper.TestFramework;
    using JetBrains.Util;

    [TestReferences("System.Reactive.Interfaces",
        "System.Reactive.Core",
        "System.Reactive.Linq",
        "System.Reactive.PlatformServices",
        "Microsoft.Reactive.Testing",
        "Resharper.ReactivePlugin.Tests.Classes")]
    public abstract class ReactiveQuickFixAvailabilityTestBase : QuickFixAvailabilityTestBase
    {
        public IDisposable ResolverReactiveAssemblies()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            Debug.WriteLine("ExtraAssemblyResolveFoldersCookie - " + currentDirectory);

            return new ExtraAssemblyResolveFoldersCookie(new FileSystemPath(currentDirectory));
        }
    }
}