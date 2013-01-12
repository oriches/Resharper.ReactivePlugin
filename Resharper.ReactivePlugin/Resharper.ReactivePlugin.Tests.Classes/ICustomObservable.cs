namespace Resharper.ReactivePlugin.Tests.Classes
{
    using System;

    public interface ICustomObservable<out T> : IObservable<T>
    {
    }
}