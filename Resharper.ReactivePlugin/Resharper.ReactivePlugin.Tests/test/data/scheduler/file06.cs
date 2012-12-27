namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System.Reactive;
    using System.Reactive.Subjects;

    public class File06
    {
        public static void Main()
        {
            new ReplaySubject<Unit>(42);
        }
    }
}
