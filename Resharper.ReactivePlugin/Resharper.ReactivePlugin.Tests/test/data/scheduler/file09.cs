namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System;
    using System.Reactive;
    using System.Reactive.Concurrency;
    using Classes;

    public class File09
    {
        private IObservable<Unit> Main()
        {
            var custom = new Custom(Scheduler.CurrentThread);
            return custom.MethodWithOptionalScheduler(Scheduler.CurrentThread);
        }
    }
}
