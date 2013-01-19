namespace Resharper.ReactivePlugin.Tests.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Reactive;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;

    public class Custom
    {
        private readonly IScheduler _scheduler;

        public Custom()
        {
            _scheduler = Scheduler.Immediate;
        }

        public Custom(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public IObservable<Unit> MethodWithOptionalScheduler(IScheduler scheduler = null)
        {
            return Observable.Return(Unit.Default, scheduler ?? _scheduler);
        }

        public IObservable<Unit> OverloadedMethodHasOptionalScheduler()
        {
            return Observable.Return(Unit.Default, _scheduler);
        }

        public IObservable<Unit> OverloadedMethodHasOptionalScheduler(int number, string text, double number2)
        {
            return Observable.Return(Unit.Default, _scheduler);
        }

        public IObservable<Unit> OverloadedMethodHasOptionalScheduler(int number, string text, IScheduler scheduler = null)
        {
            return Observable.Return(Unit.Default, scheduler ?? _scheduler);
        }

        public IObservable<Unit> OverloadedMethodHasOptionalScheduler(int number, IScheduler scheduler = null)
        {
            return Observable.Return(Unit.Default, scheduler ?? _scheduler);
        }

        public IObservable<Unit> SimpleMethod()
        {
            return Observable.Return(Unit.Default);
        }

        public IObservable<Unit> ComplexMethod(int number, IScheduler scheduler = null)
        {
            return Observable.Return(Unit.Default);
        }
    }
} ;