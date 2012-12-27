namespace Resharper.ReactivePlugin.Tests.test.data.as_observable
{
    using System;
    using System.Reactive;
    using Classes;

    public class File14
    {
        public File14()
        {
            Property = new CustomObservable<Unit>();
        }

        public IObservable<Unit> Property { get; private set; }
    }
}
