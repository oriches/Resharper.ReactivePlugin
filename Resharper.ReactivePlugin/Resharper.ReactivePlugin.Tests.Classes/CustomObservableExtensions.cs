namespace Resharper.ReactivePlugin.Tests.Classes
{
    using System;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;

    public static class CustomObservableExtensions
    {
        public static IObservable<T> OneMinuteDelay<T>(this IObservable<T> observable, IScheduler scheduler = null)
        {
            return observable.Delay(TimeSpan.FromMinutes(1), scheduler);
        }

        public static IObservable<T> TwoMinuteDelay<T>(this IObservable<T> observable)
        {
            return observable.Delay(TimeSpan.FromMinutes(1), Scheduler.CurrentThread);
        }
    }
}