namespace Resharper.ReactivePlugin.Tests.test.data.as_observable
{
    using System;
    using System.Reactive;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;

    public class File19
    {
        public File19()
        {
            Property = new ReplaySubject<Unit>(42, Scheduler.Immediate).AsObservable();
        }

        public IObservable<Unit> Property { get; set; }
    }
}
