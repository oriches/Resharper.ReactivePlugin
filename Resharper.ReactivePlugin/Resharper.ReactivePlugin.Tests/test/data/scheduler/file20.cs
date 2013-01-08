namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System;
    using System.Reactive.Concurrency;
    using Classes;

    public class File20
    {
        public static void Main()
        {
            var custom = new Custom(Scheduler.Immediate);
            custom.OverloadedMethodHasOptionalScheduler(42, "42", 42.00).Subscribe();
        }
    }
}
