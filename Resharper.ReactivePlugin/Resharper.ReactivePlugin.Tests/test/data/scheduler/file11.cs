namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System.Reactive.Concurrency;
    using Classes;

    public class File11
    {
        private void Main()
        {
            var custom = new Custom(Scheduler.Immediate);
        }
    }
}
