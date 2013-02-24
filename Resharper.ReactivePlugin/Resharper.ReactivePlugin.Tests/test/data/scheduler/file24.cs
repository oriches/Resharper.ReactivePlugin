namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using Microsoft.Reactive.Testing;

    public class File24
    {
        private readonly TestScheduler _testScheduler = new TestScheduler();

        public void Main()
        {
// ReSharper disable InvokeAsExtensionMethod
            var test = Observable.Merge(GenerateNumbers(34), GenerateNumbers(42));
// ReSharper restore InvokeAsExtensionMethod
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
