namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System;
    using System.Reactive;
    using System.Reactive.Linq;

    public class File05
    {
        public static void Main()
        {
            Observable.Create<Unit>(o => Observable.Never<Unit>().Delay(TimeSpan.FromSeconds(1)).Subscribe(o));
        }
    }
}
