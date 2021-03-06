namespace Resharper.ReactivePlugin.Tests.test.data.as_observable
{
    using System;
    using System.Reactive;
    using System.Reactive.Linq;
    using Classes;

    public class File05
    {
        private readonly CustomObservable<Unit> _test = new CustomObservable<Unit>();

        public IObservable<Unit> Method()
        {
            return _test.AsObservable();
        }
    }
}