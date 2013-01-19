namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;

    public class File22
    {
        public static void Main()
        {
            var return1 = Observable.Return(42, NewThreadScheduler.Default);
            var return2 = Observable.Return(42, NewThreadScheduler.Default);

            return1.Merge(return2);
        }
    }
}
