namespace Resharper.ReactivePlugin.Tests.Classes
{
    using System;

    public abstract class CustomAbstractObservable<T> : ICustomObservable<T>
    {
        public abstract IDisposable Subscribe(IObserver<T> observer);
    }
}