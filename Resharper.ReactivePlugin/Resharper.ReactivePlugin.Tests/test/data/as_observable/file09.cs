namespace Resharper.ReactivePlugin.Tests.test.data.as_observable
{
    using System;
    using System.Reactive;
    using Classes;

    public class File09
    {
        private readonly IObservable<Unit> _test = new CustomObservable<Unit>();

        public IObservable<Unit> Method
        {
            get { return _test; }
        }
    }
}
