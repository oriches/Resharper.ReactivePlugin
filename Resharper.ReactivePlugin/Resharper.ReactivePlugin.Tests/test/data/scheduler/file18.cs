namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System;
    using System.Reactive.Concurrency;
    using Classes;

    public class File18
    {
        public static void Main()
        {
            var custom = new Custom(Scheduler.Immediate);
            custom.OverloadedMethodHasOptionalScheduler().Subscribe();
        }
    }
}
