namespace Resharper.ReactivePlugin.Tests.Classes
{
    using System;

    public abstract class CustomAbstractObservable<T> : IObservable<T>
    {
        public abstract IDisposable Subscribe(IObserver<T> observer);
    }
}