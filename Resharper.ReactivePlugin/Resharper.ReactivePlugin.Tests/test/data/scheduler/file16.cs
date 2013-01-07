namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System;
    using System.Reactive.Concurrency;
    using Classes;

    public class File16
    {
        public static void Main()
        {
            var custom = new Custom(Scheduler.Immediate);
            custom.ComplexMethod(GetComplexMethodParameter()).Subscribe();
        }

        private static int GetComplexMethodParameter()
        {
            return 42;
        }
    }
}
