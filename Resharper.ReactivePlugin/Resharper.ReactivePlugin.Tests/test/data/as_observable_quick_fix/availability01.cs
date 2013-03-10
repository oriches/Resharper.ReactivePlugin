namespace Resharper.ReactivePlugin.Tests.test.data.as_observable_quick_fix
{
    using System;
    using System.Reactive;
    using System.Reactive.Concurrency;
    using System.Reactive.Subjects;

    public class Availability01
    {
        private readonly ReplaySubject<Unit> _test = new ReplaySubject<Unit>(42, Scheduler.Immediate);

        public IObservable<Unit> Method()
        {
            return _test;
        }
    }
}
