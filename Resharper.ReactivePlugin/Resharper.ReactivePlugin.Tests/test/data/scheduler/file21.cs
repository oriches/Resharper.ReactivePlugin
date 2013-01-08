namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;

    public class File21
    {
        public static void Main()
        {
            // ReSharper disable InvokeAsExtensionMethod
            Observable.Merge(Observable.Return(42, NewThreadScheduler.Default), Observable.Return(42, NewThreadScheduler.Default));
            // ReSharper restore InvokeAsExtensionMethod
        }
    }
}
