namespace Resharper.ReactivePlugin.Tests.test.data.select_and_merge_quick_fix
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using Microsoft.Reactive.Testing;

    public class Availability01
    {
        private readonly TestScheduler _testScheduler = new TestScheduler();

        public IObservable<int> Method()
        {
            return new List<int> { 1, 2, 3 }
                .ToObservable(_testScheduler)
                .Timeout(TimeSpan.FromSeconds(10), _testScheduler)
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
