namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    using Classes;

    public class File14
    {
        private void Main()
        {
            var tmp = Observable.Return(42, Scheduler.Immediate);
            tmp.OneMinuteDelay(Scheduler.Immediate);
        }
    }
}
