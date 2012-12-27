namespace Resharper.ReactivePlugin.Tests.test.data.as_observable
{
    using System;
    using System.Reactive;
    using System.Reactive.Linq;
    using Classes;

    public class File17
    {
        public File17()
        {
            Property = new CustomObservable<Unit>().AsObservable();
        }

        public IObservable<Unit> Property { get; set; }
    }
}
