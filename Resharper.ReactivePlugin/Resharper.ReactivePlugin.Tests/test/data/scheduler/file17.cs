namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System;
    using System.Reactive.Concurrency;
    using Classes;

    public class File17
    {
        public static void Main()
        {
            var ComplexMethod = new Custom(Scheduler.Immediate);
            ComplexMethod.ComplexMethod(GetComplexMethodParameter()).Subscribe();
        }

        private static int GetComplexMethodParameter()
        {
            return 42;
        }
    }
}
