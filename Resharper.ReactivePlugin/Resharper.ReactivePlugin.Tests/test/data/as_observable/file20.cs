namespace Resharper.ReactivePlugin.Tests.test.data.as_observable
{
    using System;
    using System.Reactive;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;

    public class File20
    {
        public IObservable<Unit> Method()
        {
            return Observable.Return(Unit.Default, Scheduler.Immediate);
        }
    }
}
