namespace Resharper.ReactivePlugin.Tests.test.data.as_observable
{
    using System;
    using System.Reactive;
    using Classes;

    public class File14
    {
        public File14()
        {
            AutoProperty = new CustomObservable<Unit>();
        }

        public IObservable<Unit> AutoProperty { get; private set; }
    }
}
