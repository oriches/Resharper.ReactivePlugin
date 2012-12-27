namespace Resharper.ReactivePlugin.Tests.test.data.as_observable
{
    using System;
    using System.Reactive;
    using System.Reactive.Concurrency;
    using System.Reactive.Subjects;

    public class File15
    {
        public File15()
        {
            Property = new ReplaySubject<Unit>(42, Scheduler.Immediate);
        }

        private IObservable<Unit> Property { get; set; }
    }
}
