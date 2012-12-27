namespace Resharper.ReactivePlugin.Tests.Classes
{
    using System;
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

        public IObservable<Unit> SimpleMethod()
        {
            return Observable.Return(Unit.Default);
        }
    }
} ;