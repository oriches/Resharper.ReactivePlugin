namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System;

    public class file00
    {
        public static double Method()
        {
            return Math.Round(42.424242, 2);
        }

        public static string Method2()
        {
            return "42" + ".42424242";
        }

        public static object Method3()
        {
            return new object();
        }
    }
}