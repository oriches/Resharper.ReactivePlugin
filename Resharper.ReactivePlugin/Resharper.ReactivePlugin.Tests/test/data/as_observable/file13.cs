namespace Resharper.ReactivePlugin.Tests.test.data.as_observable
{
    using System;
    using System.Reactive;
    using System.Reactive.Concurrency;
    using System.Reactive.Subjects;

    public class File13
    {
        public File13()
        {
            AutoProperty = new ReplaySubject<Unit>(42, Scheduler.Immediate);
        }

        public IObservable<Unit> AutoProperty { get; private set; }
    }
}
