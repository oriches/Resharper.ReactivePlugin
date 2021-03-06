namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using Microsoft.Reactive.Testing;

    public class File23
    {
        private readonly TestScheduler _testScheduler = new TestScheduler();
        private readonly TimeSpan _timeout = TimeSpan.FromSeconds(10);

        public IObservable<int> Method()
        {
            return new List<int> { 1, 2, 3 }
               .ToObservable(_testScheduler)
               .Timeout(_timeout, _testScheduler)
               .Select(GenerateNumbers)
               .Merge();
        }

        private IObservable<int> GenerateNumbers(int number)
        {
            var results = new List<int>();
            for (var i = number; i > 0; i--)
            {
                results.Add(1);
            }

            return results.ToObservable(_testScheduler);
        }
    }
}
