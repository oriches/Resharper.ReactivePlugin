namespace Resharper.ReactivePlugin.Tests
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using JetBrains.ProjectModel.Test.Components;
    using JetBrains.ReSharper.Daemon.CSharp;
    using JetBrains.ReSharper.TestFramework;
    using JetBrains.Util;

    [TestReferences("System.Reactive.Interfaces",
        "System.Reactive.Core", 
        "System.Reactive.Linq",
        "System.Reactive.PlatformServices",
        "Resharper.ReactivePlugin.Tests.Classes")]
    public abstract class ReactiveCSharpHighlightingTestBase : CSharpHighlightingTestBase
    {
        public IDisposable ResolverReactiveAssemblies()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            Debug.WriteLine("ExtraAssemblyResolveFoldersCookie - " + currentDirectory);

            return new ExtraAssemblyResolveFoldersCookie(new FileSystemPath(currentDirectory));
        }
    }
}