namespace Resharper.ReactivePlugin.Tests.test.data.as_observable
{
    using System;
    using System.Reactive;
    using System.Reactive.Subjects;

    public class File23
    {
        private readonly Subject<Unit> _test = new Subject<Unit>();

        public Subject<Unit> Property
        {
            get { return _test; }
        }
    }
}
