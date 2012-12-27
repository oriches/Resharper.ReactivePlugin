namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System;
    using System.Reactive.Linq;

    public class File04
    {
        public static void Main()
        {
            Observable.Return(42).Delay(TimeSpan.FromSeconds(1)).Select(n => n);
        }
    }
}
