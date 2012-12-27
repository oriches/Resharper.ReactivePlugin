namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System.Reactive;
    using System.Reactive.Linq;

    public class File03
    {
        public static void Main()
        {
            Observable.Create<Unit>(o => Observable.Never<Unit>().Subscribe(o));
        }
    }
}
