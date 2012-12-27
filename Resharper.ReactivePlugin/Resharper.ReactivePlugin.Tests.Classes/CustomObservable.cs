namespace Resharper.ReactivePlugin.Tests.Classes
{
    using System;
    using System.Reactive.Subjects;

    public class CustomObservable<T> : CustomAbstractObservable<T>
    {
        private readonly Subject<T> _subject = new Subject<T>();

        public override IDisposable Subscribe(IObserver<T> observer)
        {
            return _subject.Subscribe(observer);
        }
    }
}