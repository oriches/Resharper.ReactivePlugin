namespace Resharper.ReactivePlugin.Tests.test.data.as_observable
{
    using System;
    using System.Reactive;
    using Classes;

    public class File06
    {
        private readonly CustomObservable<Unit> _test = new CustomObservable<Unit>();

        private IObservable<Unit> Method()
        {
            return _test;
        }
    }
}
