namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System;
    using System.Reactive;
    using System.Reactive.Concurrency;
    using Classes;

    public class File08
    {
        private IObservable<Unit> Main()
        {
            var custom = new Custom(Scheduler.Immediate);
            return custom.MethodWithOptionalScheduler();
        }
    }
}
