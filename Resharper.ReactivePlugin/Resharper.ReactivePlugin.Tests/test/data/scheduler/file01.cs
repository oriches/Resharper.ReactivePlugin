namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System.Reactive.Linq;

    public class File01
    {
        public static void Main()
        {
            Observable.Return(42).Select(n => n);
        }
    }
}
