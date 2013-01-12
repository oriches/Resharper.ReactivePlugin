namespace Resharper.ReactivePlugin.Tests.test.data.as_observable
{
    using System.Reactive;
    using Classes;

    public class File26
    {
        public File26()
        {
            Test = new CustomObservable<Unit>();
        }

        public CustomObservable<Unit> Test { get; private set; } 
    }
}
