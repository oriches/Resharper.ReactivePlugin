namespace Resharper.ReactivePlugin.Tests.test.data.as_observable
{
    using System.Reactive;
    using System.Reactive.Subjects;

    public class File27
    {
        public File27()
        {
            Test = new Subject<Unit>();
        }

        public Subject<Unit> Test { get; private set; } 
    }
}