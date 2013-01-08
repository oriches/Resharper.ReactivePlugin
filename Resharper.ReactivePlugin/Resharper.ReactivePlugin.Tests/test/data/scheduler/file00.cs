namespace Resharper.ReactivePlugin.Tests.test.data.scheduler
{
    using System;
    using Classes;

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

        public static object Method4()
        {
            var answer = 42.Add(42);

            return answer;
        }

        public static object Method5()
        {
// ReSharper disable InvokeAsExtensionMethod
            var answer = CustomExtensions.Add(42, 42);
// ReSharper restore InvokeAsExtensionMethod

            return answer;
        }
    }
}