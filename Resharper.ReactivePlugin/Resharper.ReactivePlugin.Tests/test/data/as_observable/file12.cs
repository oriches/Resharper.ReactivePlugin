namespace Resharper.ReactivePlugin.Tests.test.data.as_observable
{
    using System;
    using System.Reactive;
    using System.Reactive.Concurrency;
    using System.Reactive.Subjects;

    public class File12
    {
        private readonly ReplaySubject<Unit> _test = new ReplaySubject<Unit>(42, Scheduler.Immediate);

        private IObservable<Unit> Method
        {
            get { return _test; }
        }
    }
}
